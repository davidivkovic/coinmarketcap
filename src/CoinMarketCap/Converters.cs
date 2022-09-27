namespace CoinMarketCap;

using CoinGecko.Entities.Response.Coins;
using CoinMarketCap.Stores;
using CoinMarketCap.Views;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

public class ToUpper : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null) return string.Empty;
        return value.ToString().ToUpper();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class CurrencyVolumeToVolume : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var markets = value as CoinMarkets;
        return string.Format(
            CultureInfo.CurrentCulture,
            "{0:N0} {1}",
            markets.TotalVolume / markets.CurrentPrice,
            markets.Symbol.ToUpper()
        );
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class SupplyWithSymbol : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var markets = value as CoinMarkets;
        return string.Format(
            CultureInfo.CurrentCulture,
            "{0:N0} {1}",
            markets.CirculatingSupply,
            markets.Symbol.ToUpper()
        );
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class SparklineToSvg : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        culture = CultureInfo.InvariantCulture;

        int index = 0;
        var markets = value as CoinMarkets;
        var max = markets.SparklineIn7D.Price.Max();
        var avg = markets.SparklineIn7D.Price.Average();
        var coef = max - avg;
        var scale = 14 / (coef == 0 ? 14 : coef);

        string path = markets.SparklineIn7D.Price.Aggregate("", (path, price) =>
        {
            char instruction = index == 0 ? 'M' : 'L';
            path = string.Format(
                culture,
                "{0} {1}{2} {3:0.###}",
                path,
                instruction,
                index,
                (max - price) * scale
            );
            index++;
            return path;
        });
        return path;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class SparklineToBrush : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var markets = value as CoinMarkets;
        if (markets.SparklineIn7D.Price.First() > markets.SparklineIn7D.Price.Last())
        {
            return "#D6455D";
        }
        return "#4fc280";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class SortToText : IMultiValueConverter
{
    private static string Prefix(ListSortDirection direction) => direction == ListSortDirection.Ascending ? "⏶" : "⏷";

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var column = values[0] as string;
        var sortedBy = values[1] as string;
        var sortDirection = (ListSortDirection)values[2];
        var alignment = HorizontalAlignment.Right;
        if (values[3] is HorizontalAlignment a) alignment = a;

        string format = alignment == HorizontalAlignment.Right ? $"{Prefix(sortDirection)}{column}" : $"{column}{Prefix(sortDirection)}";
        return sortedBy == column ? format : column;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class PercentageToTrend : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (decimal)value >= 0 ? "increase" : "decrease";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class PercentageToText : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null) return string.Empty;
        decimal percentage = (decimal)value;
        return (percentage >= 0 ? "⏶" : "⏷") + Math.Abs(percentage / 100).ToString("P", CultureInfo.InvariantCulture);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class PercentageToBrush : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (decimal)value >= 0 ? "#16c784" : "#ea3943";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class DecimalToVariablePrecision : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var price = (decimal)value;

        if (price <= 0) return string.Empty;

        int precision;
        if (price >= 1) precision = 2;
        else if (price > 0.099M) precision = 4;
        else precision = (int)Math.Log10((double)(1M / price)) * 2;
        return string.Format(CultureInfo.CurrentCulture, "{0:N" + precision + "}", price);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class DecimalToVariableCurrency : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null) return string.Empty;

        var price = (decimal)value;

        int precision;
        if (price >= 1 || price < 0) precision = 2;
        else if (price > 0.099M) precision = 4;
        else precision = (int)Math.Log10((double)(1M / price)) * 2;
        return string.Format(CultureInfo.CurrentCulture, "{0:C" + precision + "}", price);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class DecimalToShortCurrency : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        decimal v = (decimal)value;
        culture = CultureInfo.CurrentCulture;
        string symbol = culture.NumberFormat.CurrencySymbol;
        return v switch
        {
            > 999_999_999 => v.ToString(symbol + "0,,,.##B", culture),
            > 999_999 => v.ToString(symbol + "0,,.##M", culture),
            > 9_999 => v.ToString(symbol + "0,.##K", culture),
            _ => v.ToString("C", culture)
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class DecimalToZeroCurrency : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ((decimal)value).ToString("C0", CultureInfo.CurrentCulture);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class ChartHeightToMaxHeight : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (double)value + 20;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class DayToFontWeight : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string datetime = (string)value;

        if (datetime is null) return string.Empty;

        if (int.TryParse(datetime[..2], out _) 
            && (datetime.Length > 2 && (datetime[1] == ' ' || datetime[2] == ' '))
            || (datetime.Length >= 3 && datetime[..3].All(char.IsLetter))
        ) return FontWeights.SemiBold;
        return FontWeights.Medium;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class SelectedRangeToBackground : IMultiValueConverter
{
    readonly BrushConverter bc = new();

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values[0] is not TimeRange parameterRange ||
            values[1] is not TimeRange selectedRange
        ) return Brushes.White;

        var bg = DarkModeStore.Store.IsDarkMode ? "#161616" : "#f8fafd";
        var selectedBg = DarkModeStore.Store.IsDarkMode ? "#2c2c2c" : "#ffffff";

        var color = parameterRange.Range == selectedRange.Range ? selectedBg : bg;
        return (SolidColorBrush)bc.ConvertFrom(color);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class DecimalDivision : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        return string.Format(
            CultureInfo.CurrentCulture, 
            "{0:N5}", 
            values[0] is not decimal a || values[1] is not decimal b ? 0 : b / a)
       ;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class NullToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value == null ? Visibility.Collapsed : Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class CurrencyToVisibility : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values[0] is not string selectedCurrency ||
            values[1] is not string parameterCurrency
        ) return Visibility.Collapsed;

        return selectedCurrency == parameterCurrency ? Visibility.Visible : Visibility.Collapsed;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class DarkModeToBrush : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter is string pipeDelimitedColors)
        {
            var colors = pipeDelimitedColors.Split('|');
            return DarkModeStore.Store.IsDarkMode ? colors[1] : colors[0];
        }
        return "#FCFF00";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}