namespace CoinGecko.Entities.Response.Global;

using System.Text.Json.Serialization;

public class GlobalDeFi
{
    [JsonPropertyName("data")]
    public GlobalDeFiData Data { get; set; }
}

public class GlobalDeFiData
{
    [JsonPropertyName("defi_market_cap")]
    public decimal? DeFiMarketCap { get; set; }

    [JsonPropertyName("eth_market_cap")]
    public decimal? EthMarketCap { get; set; }

    [JsonPropertyName("defi_to_eth_ratio")]
    public decimal? DefiToEthRatio { get; set; }

    [JsonPropertyName("trading_volume_24h")]
    public decimal? TradingVolume24H { get; set; }

    [JsonPropertyName("defi_dominance")]
    public decimal? DeFiDominance { get; set; }

    [JsonPropertyName("top_coin_name")]
    public string TopCoinName { get; set; }

    [JsonPropertyName("top_coin_defi_dominance")]
    public decimal? TopCoinDeFiDominance { get; set; }
}
