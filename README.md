# CoinMarketCap
A simple yet beautiful cryptocurrency tracker inspired by [coinmarketcap.com](https://coinmarketcap.com/) and built using WPF

## Built using 💻🚀
- [.NET 6](https://dotnet.microsoft.com/en-us/)
- [WPF](https://github.com/dotnet/wpf)
- [CoinGecko API](https://github.com/tosunthex/CoinGecko)
- [CommunityToolkit.Mvvm](https://github.com/CommunityToolkit/dotnet)
- [PropertyChanged.Fody](https://github.com/Fody/PropertyChanged/)
- [Kasay.DependencyProperty.WPF.Fody](https://github.com/robinzevallos/Kasay.DependencyProperty.WPF.Fody)
- [FontAwesome5](https://github.com/MartinTopfstedt/FontAwesome5)
- [Inter Font](https://rsms.me/inter/)

# Showcase 📸
![matic-eur-dark](/images/matic-eur-dark.png)
![home-dark](/images/home-dark.png)
![home-light](/images/home-light.png)
![ltc-usd-light](/images/ltc-usd-light.png)
![currencies-light](/images/currencies-light.png)

## Info ℹ️
This project was initially developed during the human-computer interaction course of 2022 at the Faculty of Technical Sciences, Novi Sad.

Due to the positive reception by the teacher's assistant, it is now open-source and available for download.

- This application implements a custom high-performance charting solution, based on drawing paths to a canvas.
- Multiple coins, including real-time market data and sparklines are available through pagination on the homepage.
- Two currencies can be chosen from the top right hand dropdown.
- Clicking a currency opens a new window with a detailed view, including the aforementioned chart.

### Remarks ⁉️
- Due to the use of the free public CoinGecko API, calls are limited to 50 per minute. The application displays according waiting messages when API quotas are hit.
- The included CoinGecko API Client has had its' dependency on `Newtonsoft.Json` removed and replaced with `System.Text.Json`, increasing JSON parsing performance.
- This application supports dark mode and automatically adapts to the system settings for the default app mode, even during runtime.
- This application was built and tested using Windows 11. It is not guaranteed to work on older versions of Windows.

## Getting Started 🛠️
1. Clone the project using git or by downloading the source code
2. Open the solution file using Visual Studio 2022 or the latest version of Jetbrains Rider
3. Restore NuGet Packages, Build and Run the application

## Legal ⚖️
I am not affiliated, associated, authorized, endorsed by, or in any way officially connected to coinmarketcap.com.

All product and company names are trademarks™ or registered® trademarks of their respective holders. Use of them does not imply any affiliation with or endorsement by them.
