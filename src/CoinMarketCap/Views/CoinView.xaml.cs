namespace CoinMarketCap.Views;

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.Input;
using CoinGecko.Entities.Response.Coins;
using CoinGecko.Clients;

public record PriceData(
    decimal Price,
    decimal Volume,
    decimal MarketCap,
    DateTimeOffset Timestamp,
    string Date,
    string Time
);

public record TimeRange(TimeSpan Range, string Name);

public partial class CoinView
{
    public string Currency { get; set; }
    public CoinMarkets CoinData { get; private set; }
    public Thickness YAxisMargin { get; private set; }
    public Thickness YOpenMargin { get; private set; }
    public string YPointPrice { get; private set; }
    public Point ChartPoint { get; set; }
    private Dictionary<int, Point> PathPoints { get; set; } = new();
    public ObservableCollection<string> DescriptionRows { get; set; } = new();
    public Visibility LoadingVisibility { get; set; } = Visibility.Collapsed;
    public double LoadingOpacity { get; set; } = 1;
    public ObservableCollection<string> Timesteps { get; set; } = new();
    public TimeRange TimeRange { get; set; } = new(TimeSpan.FromDays(30), "30D");
    public List<TimeRange> TimeRangeNames { get; } = new()
    {
        new TimeRange(TimeSpan.FromDays(1), "1D"),
        new TimeRange(TimeSpan.FromDays(7), "7D"),
        new TimeRange(TimeSpan.FromDays(30), "1M"),
        new TimeRange(TimeSpan.FromDays(90), "3M"),
        new TimeRange(TimeSpan.FromDays(365), "1Y")
    };
    public string FormattedOpen => FormatMixed(Open, CultureInfo.CurrentCulture);
    readonly DecimalToVariablePrecision _variablePrecisionConverter = new();
    public RelayCommand<TimeRange> ChangeTabCommand { get; set; }
    public RelayCommand DismissErrorCommand { get; set; }

    public PriceData PriceAtPoint
    {
        get
        {
            var price = PriceData.GetValueOrDefault(decimal.Round((decimal)ChartPoint.X, 3));
            if (price is not null)
            {
                _priceAtPoint = price;
            }

            return _priceAtPoint;
        }
    }

    public string ChartPointFill => PriceAtPoint?.Price >= Open ? "#16C784" : "#EA3943";

    public string Sparkline
    {
        get
        {
            if (Data is null || !ResyncChart) return string.Empty;

            decimal index = 0;
            var scale = _hResolution / (Max - Min);

            return Data.Prices.Aggregate("", (path, price) =>
            {
                var instruction = index == 0 ? 'M' : 'L';
                path = string.Format(
                    CultureInfo.InvariantCulture,
                    "{0} {1}{2} {3:0.###}",
                    path,
                    instruction,
                    index,
                    decimal.Round((Max - price?[1] ?? 0) * scale, 3)
                );
                index += Step;
                return path;
            }) +
            string.Format(
                CultureInfo.InvariantCulture,
                " {0}{1} {2:0.###}",
                'L',
                index,
                (Max - Open) * scale
            );
        }
    }

    public Brush Stroke
    {
        get
        {
            if (Data is null) return Brushes.Transparent;

            var ratio = (double)(1 - (Open - Min) / (Max - Min));

            var green = (Color)ColorConverter.ConvertFromString("#16C784")!;
            var red = (Color)ColorConverter.ConvertFromString("#EA3943")!;

            GradientStopCollection collection = new()
            {
                new GradientStop(green, 0),
                new GradientStop(green, ratio),
                new GradientStop(red, ratio),
                new GradientStop(red, 1.0)
            };

            return new LinearGradientBrush(collection, angle: 90);
        }
    }

    public Brush Fill
    {
        get
        {
            if (Data is null) return Brushes.Transparent;

            var ratio = (double)(1 - (Open - Min) / (Max - Min));

            var green = (Color)ColorConverter.ConvertFromString("#16C784");
            var red = (Color)ColorConverter.ConvertFromString("#EA3943");

            GradientStopCollection collection = new();

            collection.Add(new GradientStop(green, -0.95));
            collection.Add(new GradientStop(Color.FromArgb(3, 131, 214, 183), ratio));
            collection.Add(new GradientStop(Color.FromArgb(3, 247, 153, 159), ratio));
            collection.Add(new GradientStop(red, 1.95));

            return new LinearGradientBrush(collection, angle: 90);
        }
    }

    public List<string> YPrices
    {
        get
        {
            if (Data is null) return new();

            var step = (Max - Min) / 6;

            return Enumerable.Range(0, 7)
                .Select(i => Max - i * step)
                .Select(v => FormatMixed(v, CultureInfo.CurrentCulture))
                .ToList();
        }
    }

    public List<int> VolumeBarHeights
    {
        get
        {
            if (Data is null) return new();

            var max = (decimal)Data.TotalVolumes.MaxBy(p => p[1])[1];
            var chunkSize = Data.TotalVolumes.Length / 140;

            return Data.TotalVolumes
                .Chunk(chunkSize)
                .Select(c => c.First())
                .Select(v => (int)(v[1] / max * 60))
                .ToList();
        }
    }

    public string VolumeSparkline
    {
        get
        {
            if (Data is null) return string.Empty;

            decimal index = 0;
            var max = (decimal)Data.TotalVolumes.MaxBy(p => p[1])[1];
            var min = (decimal)Data.TotalVolumes.MinBy(p => p[1])[1];
            var avg = (decimal)Data.TotalVolumes.Average(p => p[1]);

            var scale = 25 / (max - min);

            return $"M0 {(max - min) * scale} " + Data.TotalVolumes.Aggregate("", (path, volume) =>
            {
                const char instruction = 'L';
                path = string.Format(
                    CultureInfo.InvariantCulture,
                    "{0} {1}{2} {3:0.####}",
                    path,
                    instruction,
                    index,
                    (max - volume[1]) * scale - 12
                );
                index += Step;
                return path;
            }) + $" L{index + 3} {(max - min) * scale}";
        }
    }

    private const decimal _hResolution = 360;
    private const decimal _wResolution = 740;
    private readonly CoinGeckoClient _api = new();
    private readonly string _coin;
    private PriceData _priceAtPoint;
    private MarketChartById Data { get; set; }
    private Dictionary<decimal, PriceData> PriceData { get; set; } = new();
    private decimal Step { get; set; } = 2.75m;
    private decimal Min { get; set; }
    private decimal Max { get; set; }
    private decimal Open { get; set; }
    private bool ResyncChart { get; set; }
    private TimeRange OldTimeRange { get; set; } = new(TimeSpan.FromDays(30), "30D");
    
    string FormatMixed(decimal number, CultureInfo culture)
    {
        return number switch
        {
            > 9_999 => number.ToString("0,.00K", culture),
            > 999 => number.ToString("N0", culture),
            _ => (string)_variablePrecisionConverter.Convert(number, typeof(string), null, culture)
        };
    }

    private async Task FetchChart(bool initial = false)
    {
        Loading(initial);
        var from = DateTimeOffset.UtcNow - TimeRange.Range;
        var to = DateTimeOffset.UtcNow;
        MarketChartById d;

        try
        {
            d = await _api.CoinsClient.GetMarketChartRangeByCoinId(
                id: _coin,
                vsCurrency: Currency,
                from.ToUnixTimeSeconds().ToString(),
                to.ToUnixTimeSeconds().ToString()
            );
        }
        catch (HttpRequestException)
        {
            DismissErrorCommand = new RelayCommand(() => DoneLoading(shouldClose: initial));
            Error();
            return;
        }

        Step = _wResolution / d.Prices.Length;

        var open = (decimal)d.Prices[0][1];
        var max = (decimal)d.Prices.MaxBy(p => p[1])[1];
        var min = (decimal)d.Prices.MinBy(p => p[1])[1];
        var avg = (decimal)d.Prices.Average(p => p?[1]);

        var dateTimeCulture = new CultureInfo("en-US");

        await Dispatcher.InvokeAsync(() =>
        {
            ResyncChart = false;
            PriceData = new();

            for (var i = 0; i < d.Prices.Length; i++)
            {
                var timestamp = DateTimeOffset.FromUnixTimeMilliseconds((long)d.Prices[i][0]) + TimeSpan.FromHours(2);
                PriceData.Add(
                    decimal.Round(i * Step, 3),
                    new PriceData(
                        (decimal)d.Prices[i][1],
                        (decimal)d.TotalVolumes[i][1],
                        (decimal)d.MarketCaps[i][1],
                        timestamp,
                        timestamp.DateTime.ToString("d", dateTimeCulture),
                        timestamp.DateTime.ToString("T", dateTimeCulture)
                    )
                );
            }

            Open = open;
            Max = max;
            Min = min;
            Data = d;
            YAxisMargin = new Thickness(0, 0, 0, (double)decimal.Round(_hResolution / (max - min) * (max - min) / 7, 3) + 2);
            YOpenMargin = new Thickness(0, (double)(_hResolution / (max - min)) * (double)(max - open) + 24, 0, 0);

            ResyncChart = true;

            PathPoints = ChartPath.Data
            .GetFlattenedPathGeometry()
            .Figures.SelectMany(f => f.Segments)
            .SelectMany(s => ((PolyLineSegment)s).Points)
            .DistinctBy(p => (int)p.X)
            .ToDictionary(p => (int)p.X, p => p);

            int steps = TimeRange.Range.TotalDays switch
            {
                >= 365 => 12,
                >= 7 => 7,
                1 or _ => 8
            };

            var timestep = (to - from) / steps;

            var timeFormat = TimeRange.Range.TotalDays switch
            {
                1 => "h:mm tt",
                >= 7 => "MMM d",
                _ => "T"
            };

            Timesteps.Clear();

            var dateTimeSteps = Enumerable.Range(0, steps + 1)
                              .Select(i => from + TimeSpan.FromHours(3) + i * timestep)
                              .ToList();

            for (var i = 0; i < dateTimeSteps.Count - 1; i++)
            {
                if (TimeRange.Range >= TimeSpan.FromDays(7) &&
                    dateTimeSteps[i].Month != dateTimeSteps[i + 1].Month
                )
                {
                    Timesteps.Add(dateTimeSteps[i + 1].ToString("MMM"));
                }
                else if (TimeRange.Range == TimeSpan.FromDays(1) &&
                         dateTimeSteps[i].DayOfYear != dateTimeSteps[i + 1].DayOfYear
                )
                {
                    Timesteps.Add(dateTimeSteps[i + 1].ToString("MMM d"));
                }
                else
                {
                    Timesteps.Add(dateTimeSteps[i].ToString(timeFormat, dateTimeCulture));
                }
            }

            DoneLoading();
        });
    }

    private async Task FetchData()
    {
        var data = (await _api.CoinsClient.GetCoinMarkets(
            vsCurrency: Currency,
            ids: new[] { _coin },
            order: "market_cap_desc",
            perPage: 1,
            page: 0,
            sparkline: false,
            priceChangePercentage: "24h,7d",
            category: ""
        )).FirstOrDefault();

        Dispatcher.Invoke(() => CoinData = data);
    }

    private static IEnumerable<string> StringToTextBlocks(string input)
    {
        var tagFound = false;
        StringBuilder sb = new();

        foreach (var t in input)
        {
            if (t == '<') tagFound = true;
            if (!tagFound) sb.Append(t);
            if (t == '>') tagFound = false;
        }

        return sb.ToString().Split("\r\n\r\n");
    }

    private void Loading(bool initial = false)
    {
        Dispatcher.Invoke(() => 
        {   
            LoadingTitle.Text = "Loading Data";
            LoadingDescription.Text = "Please wait, we are loading coin data";
            if (initial) LoadingOpacity = 1;
            else LoadingOpacity = 0.9;
            DismissErrorButton.Visibility = Visibility.Collapsed;
            LoadingIcon.Visibility = Visibility.Visible;
            LoadingVisibility = Visibility.Visible;
        });
    }

    private void DoneLoading(bool shouldClose = false)
    {
        if (shouldClose)
        {
            Close();
            return;
        }
        Dispatcher.Invoke(() =>
        {
            LoadingVisibility = Visibility.Collapsed;
        });
    }

    private void Error()
    {
        Dispatcher.Invoke(() =>
        {
            TimeRange = OldTimeRange;
            LoadingTitle.Text = "Too many requests";
            LoadingDescription.Text = "Please wait for the API to be available again";
            LoadingIcon.Visibility = Visibility.Collapsed;
            DismissErrorButton.Visibility = Visibility.Visible;
            LoadingVisibility = Visibility.Visible;
        });
    }

    public CoinView(string coin, string currency)
    {
        InitializeComponent();
        FontFamily = new FontFamily(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fonts", "#Inter"));

        _coin = coin;
        Currency = currency;

        ChangeTabCommand = new RelayCommand<TimeRange>(async (TimeRange r) =>
        {
            OldTimeRange = TimeRange;
            TimeRange = r;
            await FetchChart();
        });

        DismissErrorCommand = new RelayCommand(() => DoneLoading());

        Task.Run(async () =>
        {
            var descriptionTask = _api.CoinsClient.GetAllCoinDataWithId(_coin, "false", false, false, false, false, false);
            await Task.WhenAll(FetchChart(initial: true), FetchData());

            Dispatcher.Invoke(() =>
            {
                NotifuFullyRendered();
                Title = $"CoinMarketCap - {CoinData?.Name} Price Chart";
            });

            var descriptionData = await descriptionTask;
            var descriptionRows = StringToTextBlocks(descriptionData.Description["en"]);

            Dispatcher.Invoke(() =>
            {
                foreach (var t in descriptionRows)
                {
                    DescriptionRows.Add(t);
                }
            });
        });
    }

    private void ChartMouseMove(object sender, MouseEventArgs e)
    {
        var position = e.GetPosition(ChartPath);
        var pathPoint = PathPoints.GetValueOrDefault((int)position.X);

        double width = ChartPanel.ActualWidth;
        double timestampWidth = Math.Max(XAxisTimestampPanel.ActualWidth, 150);

        if (pathPoint != default)
        {
            ChartPoint = pathPoint;

            var xTimestampMargin = XAxisTimestampPanel.Margin;
            xTimestampMargin.Left = Math.Clamp(
                position.X - timestampWidth / 2,
                0,
                width - timestampWidth - 6
            );
            XAxisTimestampPanel.Margin = xTimestampMargin;
        }

        var yPointMargin = YPointPricePanel.Margin;
        var chartTooltipMargin = ChartTooltip.Margin;

        if (position.X > width - 280)
        {
            chartTooltipMargin.Left = position.X - 220;
        }
        else
        {
            chartTooltipMargin.Left = position.X + 80;
        }

        if (position.Y < 110)
        {
            chartTooltipMargin.Top = position.Y + 52;
        }
        else
        {
            chartTooltipMargin.Top = position.Y - 92;
        }

        HorizontalChartLine.Y1 = HorizontalChartLine.Y2 = yPointMargin.Top = position.Y + 24;

        ChartTooltipWrapper.Margin = chartTooltipMargin;
        ChartTooltip.Margin = chartTooltipMargin;
        YPointPricePanel.Margin = yPointMargin;

        YPointPrice = FormatMixed(
            Min + (Max - Min) *
            (1 - (decimal)(position.Y / ChartPath.ActualHeight)),
            CultureInfo.CurrentCulture
        );
    }

    private void FollowOnTwitter(object sender, MouseButtonEventArgs e)
    {
        Process.Start(
            new ProcessStartInfo(
                "cmd", $"/c start https://twitter.com/CoinMarketCap/"
            )
            { CreateNoWindow = true }
        );
    }
}