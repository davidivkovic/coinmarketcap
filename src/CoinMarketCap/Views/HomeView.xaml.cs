namespace CoinMarketCap.Views;

using System.IO;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CoinGecko.Clients;
using CoinGecko.Entities.Response.Coins;

public static class Extensions
{
    public static List<T> Sort<T, TKey>(
       this IEnumerable<T> list,
       Func<T, TKey> keyExtractor,
       ListSortDirection direction
    )
    {
        return direction == ListSortDirection.Ascending ?
            list.OrderBy(keyExtractor).ToList() :
            list.OrderByDescending(keyExtractor).ToList();
    }

    public static void Sort<T, TKey>(
        this ObservableCollection<T> collection,
        Func<T, TKey> keyExtractor,
        ListSortDirection direction
    )
    {
        var sorted = direction == ListSortDirection.Ascending ?
            collection.OrderBy(keyExtractor).ToList() : 
            collection.OrderByDescending(keyExtractor).ToList();
        
        for (var i = 0; i < sorted.Count; i++)
        {
            collection.Move(collection.IndexOf(sorted[i]), i);
        }
    }
}

public record Currency(string Name, string Symbol, string Id);

public partial class HomeView
{
    private CoinGeckoClient Api { get; } = new();
    public ObservableCollection<CoinMarkets> Coins { get; set; } = new();
    public decimal MarketCapChangePercentage24h { get; set; }
    public RelayCommand<string> Sort { get; set; }
    public AsyncRelayCommand<Currency> SelectCurrency { get; set; }
    public AsyncRelayCommand DismissErrorCommand { get; set; }
    public string SortedBy { get; set; } = "#";
    public ListSortDirection SortDirection { get; set; } = ListSortDirection.Ascending;
    public int CurrentPage { get; set; } = 1;
    public Visibility LoadingVisibility { get; set; } = Visibility.Visible;
    public double BackButtonOpacity => CurrentPage == 1 ? 0 : 1;

    public static List<Currency> Currencies { get; set; } = new()
    {
        new Currency("US Dollar", "$", "usd"),
        new Currency("Euro", "€", "eur")
    };

    public Currency SelectedCurrency { get; private set; } = Currencies.FirstOrDefault();
    public bool CurrencyDropdownOpen { get; set; }
    public bool CoinsLoaded { get; set; }

    private static Func<CoinMarkets, object> KeyExtractor(string property) => property switch
    {
        "#" => c => c.MarketCapRank,
        "Name" => c => c.Name,
        "Price" => c => c.CurrentPrice,
        "24h %" => c => c.PriceChangePercentage24HInCurrency,
        "7d %" => c => c.PriceChangePercentage7DInCurrency,
        "Market Cap" => c => c.MarketCap,
        "Volume(24h)" => c => c.TotalVolume,
        "Circulating Supply" => c => c.CirculatingSupply,
        "Total Supply" => c => c.TotalSupply,
        _ => throw new NotImplementedException()
    };

    private Func<CoinMarkets, object> UpdateSortParams(string property, bool reverse)
    {
        var lambda = KeyExtractor(property);

        if (SortedBy == property && reverse)
        {
            SortDirection = SortDirection == ListSortDirection.Ascending ?
                                             ListSortDirection.Descending :
                                             ListSortDirection.Ascending;
        }
        else if (reverse)
        {
            SortDirection = ListSortDirection.Descending;
        }
        SortedBy = property;

        return lambda;
    }

    private void SortObservableCollection(string property)
    {
        var keyExtractor = UpdateSortParams(property, reverse: true);
        Coins.Sort(keyExtractor, SortDirection);
    }

    private Task<List<CoinMarkets>> GetCoins(int size, int page)
    {
        return Api.CoinsClient.GetCoinMarkets(
            vsCurrency: SelectedCurrency.Id,
            ids: Array.Empty<string>(),
            order: "market_cap_desc",
            perPage: size,
            page: page,
            sparkline: true,
            priceChangePercentage: "24h,7d",
            category: ""
        );
    }

    private void SelectCoin(object sender, MouseButtonEventArgs e)
    {
        LoadingVisibility = Visibility.Visible;
        var selectedCoinId = (string)((Panel)sender).Tag;

        var w = new CoinView(selectedCoinId, SelectedCurrency.Id)
        {
            //WindowState = WindowState.Minimized,
            Width = ActualWidth,
            Height = ActualHeight
        };
        w.FullyRendered += (_, _) =>
        {
            w.Show();
            w.Top += 25;
            w.Left += 25;

            // w.CenterWindowOnScreen();
            //w.WindowState = WindowState.Normal;
            LoadingVisibility = Visibility.Collapsed;
        };
    }

    private void ToggleCurrencyDropdown(object sender, MouseEventArgs e)
    {
        CurrencyDropdownOpen = !CurrencyDropdownOpen;
    }

    private async Task OnSelectCurrency(Currency currency)
    {
        if (SelectedCurrency == currency)
        {
            CurrencyDropdownOpen = false;
            return;
        }

        LoadingVisibility = Visibility.Visible;
        SelectedCurrency = currency;

        await Dispatcher.InvokeAsync(() =>
        {
            CurrencyDropdownOpen = false;
            var culture = currency.Id switch
            {
                "eur" => "en-IE",
                "usd" or _ => "en-US"
            };
            
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
        });

        await FetchData(CurrentPage);
    }

    private async Task FetchData(int page = 1)
    {
        try
        {
            await Dispatcher.BeginInvoke(() => 
            {
                LoadingTitle.Text = "Loading Data";
                LoadingDescription.Text = "Please wait, we are loading coin data";
                LoadingIcon.Visibility = Visibility.Visible;
                DismissErrorButton.Visibility = Visibility.Collapsed;
                LoadingVisibility = Visibility.Visible;
            }); 
            var globalTask = Api.GlobalClient.GetGlobal();
            var coins = await GetCoins(size: 30, page);
            var globalData = await globalTask;
            var keyExtractor = UpdateSortParams(SortedBy, reverse: false);

            await Dispatcher.BeginInvoke(() =>
            {
                MarketCapChangePercentage24h = globalData.Data.MarketCapChange;
                Coins.Clear();
                coins.Sort(keyExtractor, SortDirection).ForEach(c => Coins.Add(c));
                CoinsLoaded = true;
                LoadingVisibility = Visibility.Collapsed;
            });
        }
        catch (HttpRequestException)
        {
            Dispatcher.Invoke(() =>
            {
                LoadingTitle.Text = "Too many requests";
                LoadingDescription.Text = "Please wait for the API to be available again";
                LoadingIcon.Visibility = Visibility.Collapsed;
                DismissErrorButton.Visibility = Visibility.Visible;
            });
        }
    }

    private async void PreviousPage(object sender, RoutedEventArgs ea)
    {
        if (CurrentPage == 1) return;

        var page = Math.Max(1, CurrentPage - 1);
        await FetchData(page);
        Scroller.ScrollToTop();
        CurrentPage = page;
        
        Scroller.Focus();
    }

    private async void NextPage(object sender, RoutedEventArgs ea)
    {
        var page = Math.Min(CurrentPage + 1, 100);
        await FetchData(page);
        Scroller.ScrollToTop();
        CurrentPage = page;
        Scroller.Focus();
    }

    public HomeView()
    {
        Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
        InitializeComponent();
        FontFamily = new FontFamily(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fonts", "#Inter"));

        var workArea = SystemParameters.WorkArea;
        Height = workArea.Height * 0.96;
        Title = "CoinMarketCap - Homepage";

        Sort = new RelayCommand<string>(SortObservableCollection);
        SelectCurrency = new AsyncRelayCommand<Currency>(OnSelectCurrency);
        DismissErrorCommand = new AsyncRelayCommand(() => FetchData(CurrentPage));
        DismissErrorButton.Visibility = Visibility.Collapsed;

        PreviewMouseDown += (_, _) =>
        {
            if (CurrencyDropdownOpen &&
                !CurrenciesButton.IsMouseOver &&
                !CurrenciesBorder.IsMouseOver)
            {
                CurrencyDropdownOpen = false;
            }
        };

        Task.Run(async () =>
        {
            await FetchData();
        });
    }
}