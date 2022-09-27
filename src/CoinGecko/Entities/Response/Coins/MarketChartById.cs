namespace CoinGecko.Entities.Response.Coins;

using System.Text.Json.Serialization;

public class MarketChartById
{
    [JsonPropertyName("prices")]
    public decimal?[][] Prices { get; set; }

    [JsonPropertyName("market_caps")]
    public decimal?[][] MarketCaps { get; set; }

    [JsonPropertyName("total_volumes")]
    public decimal?[][] TotalVolumes { get; set; }
}