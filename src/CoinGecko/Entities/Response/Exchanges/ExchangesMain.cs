namespace CoinGecko.Entities.Response.Exchanges;

using System.Text.Json.Serialization;

public class ExchangesMain
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("has_trading_incentive")]
    public bool? HasTradingIncentive { get; set; }

    [JsonPropertyName("trust_score")]
    public double? TrustScore { get; set; }
    
    [JsonPropertyName("trust_score_rank")]
    public double? TrustScoreRank { get; set; }

    [JsonPropertyName("trade_volume_24h_btc")]
    public double? TradeVolume24HBtc { get; set; }
    
    [JsonPropertyName("trade_volume_24h_btc_normalized")]
    public double? TradeVolume24HBtcNormalized { get; set; }
}