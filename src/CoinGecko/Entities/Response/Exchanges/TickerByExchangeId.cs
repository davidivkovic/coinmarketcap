namespace CoinGecko.Entities.Response.Exchanges;

using CoinGecko.Entities.Response.Shared;
using System.Text.Json.Serialization;

public class TickerByExchangeId
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("tickers")]
    public Ticker[] Tickers { get; set; }
}