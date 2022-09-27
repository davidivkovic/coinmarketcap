namespace CoinMarketCap.Primitives;

using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using CoinMarketCap.Stores;
using Microsoft.Win32;
using PropertyChanged;

public partial class Window : System.Windows.Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public event EventHandler FullyRendered;

    public bool LightMode { get; set; }

    private bool _darkMode;

    [DoNotCheckEquality]
    public bool DarkMode 
    { 
        get => _darkMode;
        set
        {
            _darkMode = value;
            LightMode = !_darkMode;
            DarkModeStore.Store.IsDarkMode = _darkMode;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(DarkMode))); 
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(LightMode)));
        }
    }

    public IntPtr Handle { get; set; }

    protected void OnPropertyChanged(PropertyChangedEventArgs eventArgs)
    {
        PropertyChanged?.Invoke(this, eventArgs);
    }

    public void NotifuFullyRendered()
    {
        FullyRendered?.Invoke(this, EventArgs.Empty);
    }

    [DllImport("uxtheme")]
    static extern int SetWindowThemeAttribute(
        IntPtr hWnd,
        WindowThemeAttributeType wtype,
        ref WTA_OPTIONS attributes,
        uint size
    );

    [DllImport("uxtheme", SetLastError = true, EntryPoint = "#138")]
    static extern bool ShouldSystemUseDarkMode();

    [DllImport("dwmapi")]
    private static extern int DwmSetWindowAttribute(
        IntPtr hwnd,
        DwmWindomAttributeType attribute,
        [In] ref bool pvAttribute,
        int cbAttribute
    );


    private enum DwmWindomAttributeType
    {
        DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
    }

    public enum WindowThemeAttributeType : uint
    {
        WTA_NONCLIENT = 1,
    }

    public struct WTA_OPTIONS
    {
        public uint Flags;
        public uint Mask;
    }

    static readonly uint WTNCA_NODRAWCAPTION = 0x00000001;
    readonly static uint WTNCA_NODRAWICON = 0x00000002;

    WTA_OPTIONS wta = new () 
    { 
        Flags = WTNCA_NODRAWCAPTION | WTNCA_NODRAWICON,
        Mask = WTNCA_NODRAWCAPTION | WTNCA_NODRAWICON 
    };

    public Window()
    {
        SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
        QueryIsDarkMode();
        SourceInitialized += (s, ea) =>
        {
            try
            {
                Handle = new WindowInteropHelper(this).EnsureHandle();
                CleanTitleBar();
                SetDarkMode();
            }
            catch { }
        };
    }

    private void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
    {
        QueryIsDarkMode();
        SetDarkMode();
    }

    private void QueryIsDarkMode()
    {
        try
        {
            var appsUseLightTheme = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", "1");
            DarkMode = !Convert.ToBoolean(appsUseLightTheme);
        }
        catch { }
    }

    private int SetDarkMode()
    {
        bool darkMode = DarkMode;
        return DwmSetWindowAttribute(
            Handle,
            DwmWindomAttributeType.DWMWA_USE_IMMERSIVE_DARK_MODE,
            ref darkMode,
            Marshal.SizeOf<bool>()
        );
    }

    private int CleanTitleBar()
    {
        return SetWindowThemeAttribute(
            Handle,
            WindowThemeAttributeType.WTA_NONCLIENT,
            ref wta,
            (uint)Marshal.SizeOf(typeof(WTA_OPTIONS))
        );
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        SystemEvents.UserPreferenceChanged -= SystemEvents_UserPreferenceChanged;
        base.OnClosing(e);
    }

    public void CenterWindowOnScreen()
    {
        Rect workArea = SystemParameters.WorkArea;
        Left = (workArea.Width - Width) / 2 + workArea.Left;
        Top = (workArea.Height - Height) / 2 + workArea.Top;
    }
}
