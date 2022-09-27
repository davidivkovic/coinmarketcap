namespace CoinMarketCap;

using CoinMarketCap.Primitives;
using CoinMarketCap.Views;
using System.Globalization;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

        InitializeComponent();

        Title = "CoinMarketCap - Homepage";
    }
}