namespace CoinGecko.Interfaces;

using System.Threading.Tasks;
using CoinGecko.Entities.Response.ExchangeRates;
public interface IExchangeRatesClient
{
    /// <summary>
    /// Get BTC-to-Currency exchange rates
    /// </summary>
    /// <returns></returns>
    Task<ExchangeRates> GetExchangeRates();
}