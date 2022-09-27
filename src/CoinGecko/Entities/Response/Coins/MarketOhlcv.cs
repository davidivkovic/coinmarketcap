namespace CoinGecko.Entities.Response.Coins;

using System.Text.Json.Serialization;

public class MarketDataOhlcv
{
    [JsonPropertyName("market_cap_rank")]
    public long? MarketCapRank { get; set; }

    [JsonPropertyName("price_change_24h")]
    public decimal? PriceChange24H { get; set; }

    [JsonPropertyName("price_change_percentage_24h")]
    public decimal? PriceChangePercentage24H { get; set; }

    [JsonPropertyName("market_cap_change_24h")]
    public decimal? MarketCapChange24H { get; set; }

    [JsonPropertyName("market_cap_change_percentage_24h")]
    public decimal? MarketCapChangePercentage24H { get; set; }

    [JsonPropertyName("circulating_supply")]
    public decimal? CirculatingSupply { get; set; }

    [JsonPropertyName("total_supply")]
    public decimal? TotalSupply { get; set; }
    
}