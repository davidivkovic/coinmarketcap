namespace CoinGecko.Clients;

using System.Net.Http;
using System.Threading.Tasks;
using CoinGecko.ApiEndPoints;
using CoinGecko.Entities.Response.ExchangeRates;
using CoinGecko.Interfaces;
using CoinGecko.Services;

public class ExchangeRatesClient : BaseApiClient, IExchangeRatesClient
{
    public ExchangeRatesClient(HttpClient httpClient) : base(httpClient) { }

    public async Task<ExchangeRates> GetExchangeRates()
    {
        return await GetAsync<ExchangeRates>(
            QueryStringService.AppendQueryString(ExchangeRatesApiEndPoints.ExchangeRate)).ConfigureAwait(false);
    }
}