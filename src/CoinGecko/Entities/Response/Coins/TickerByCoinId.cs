namespace CoinGecko.Entities.Response.Coins;

using CoinGecko.Entities.Response.Shared;
using System.Text.Json.Serialization;

public class TickerById
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("tickers")]
    public Ticker[] Tickers { get; set; }
}