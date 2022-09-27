namespace CoinGecko.Entities.Response.Derivatives;

using System.Text.Json.Serialization;

public class Derivatives
{
    [JsonPropertyName("market")]
    public string Market { get; set; }

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; }

    [JsonPropertyName("coin_id")]
    public string CoinId { get; set; }

    [JsonPropertyName("index_id")]
    public string IndexId { get; set; }

    [JsonPropertyName("price")]
    public string Price { get; set; }

    [JsonPropertyName("price_percentage_change_24h")]
    public double? PricePercentageChange24H { get; set; }

    [JsonPropertyName("contract_type")]
    public string ContractType { get; set; }

    [JsonPropertyName("index")]
    public double? Index { get; set; }

    [JsonPropertyName("basis")]
    public double? Basis { get; set; }

    [JsonPropertyName("spread")]
    public double? Spread { get; set; }

    [JsonPropertyName("funding_rate")]
    public double? FundingRate { get; set; }

    [JsonPropertyName("open_interest")]
    public double? OpenInterest { get; set; }

    [JsonPropertyName("volume_24h")]
    public double? Volume24H { get; set; }

    [JsonPropertyName("last_traded_at")]
    public long? LastTradedAt { get; set; }

    [JsonPropertyName("expired_at")]
    public long? ExpiredAt { get; set; }
}