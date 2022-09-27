using System.ComponentModel;

namespace CoinMarketCap.Stores;

class DarkModeStore : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private static readonly DarkModeStore _instance = new();
    private DarkModeStore() { }

    public static DarkModeStore Store
    {
        get
        {
            return _instance;
        }
    }

    public bool IsDarkMode { get; set; }
}
