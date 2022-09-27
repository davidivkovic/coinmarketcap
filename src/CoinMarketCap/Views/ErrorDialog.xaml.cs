namespace CoinMarketCap.Views;

using System.IO;
using System.Windows.Media;

public partial class ErrorDialog : Primitives.Window
{
    public ErrorDialog()
    {
        InitializeComponent();
        FontFamily = new FontFamily(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fonts", "#Inter"));
    }
}