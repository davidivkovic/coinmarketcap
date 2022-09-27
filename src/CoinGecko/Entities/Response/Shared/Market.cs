namespace CoinGecko.Entities.Response.Shared;

using System.Text.Json.Serialization;

public class Market
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("identifier")]
    public string Identifier { get; set; }

    [JsonPropertyName("has_trading_incentive")]
    public bool HasTradingIncentive { get; set; }
}