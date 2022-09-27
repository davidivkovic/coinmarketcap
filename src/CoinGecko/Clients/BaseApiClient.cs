namespace CoinGecko.Clients;

using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CoinGecko.Interfaces;

public class BaseApiClient : IAsyncApiRepository
{
    private readonly HttpClient _httpClient;

    public BaseApiClient(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<T> GetAsync<T>(Uri resourceUri)
    {
        //_httpClient.DefaultRequestHeaders.Add("User-Agent", "your bot 0.1");
        var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, resourceUri))
            .ConfigureAwait(false);
        
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        try
        {
            return JsonSerializer.Deserialize<T>(responseContent);
        }
        catch (Exception e)
        {
            throw new HttpRequestException(e.Message);
        }
    }
}