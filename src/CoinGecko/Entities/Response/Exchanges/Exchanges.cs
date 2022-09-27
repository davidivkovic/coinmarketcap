namespace CoinGecko.Entities.Response.Exchanges;

using System;
using System.Text.Json.Serialization;

public class Exchanges:ExchangesMain
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("year_established")]
    public long? YearEstablished { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("image")]
    public string Image { get; set; }
}