namespace CoinGecko.Entities.Response.Derivatives;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class DerivativesExchanges
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("open_interest_btc")]
    public double? OpenInterestBtc { get; set; }

    [JsonPropertyName("trade_volume_24h_btc")]
    public string TradeVolume24HBtc { get; set; }

    [JsonPropertyName("number_of_perpetual_pairs")]
    public long? NumberOfPerpetualPairs { get; set; }

    [JsonPropertyName("number_of_futures_pairs")]
    public long? NumberOfFuturesPairs { get; set; }

    [JsonPropertyName("image")]
    public Uri Image { get; set; }

    [JsonPropertyName("year_established")]
    public long? YearEstablished { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }
    
    [JsonPropertyName("tickers")]
    public IReadOnlyList<DerivativesTicker> Tickers { get; set; }
}