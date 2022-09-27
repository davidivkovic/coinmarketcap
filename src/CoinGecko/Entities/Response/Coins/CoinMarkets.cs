namespace CoinGecko.Entities.Response.Coins;

using System;
using System.Text.Json.Serialization;

public class CoinMarkets : MarketDataOhlcv
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("image")]
    public Uri Image { get; set; }

    [JsonPropertyName("current_price")]
    public decimal? CurrentPrice { get; set; }

    [JsonPropertyName("market_cap")]
    public decimal? MarketCap { get; set; }

    [JsonPropertyName("fully_diluted_valuation")]
    public decimal? FullyDilutedMarketCap { get; set; }

    [JsonPropertyName("total_volume")]
    public decimal? TotalVolume { get; set; }

    [JsonPropertyName("high_24h")]
    public decimal? High24H { get; set; }

    [JsonPropertyName("low_24h")]
    public decimal? Low24H { get; set; }

    [JsonPropertyName("ath")]
    public decimal? Ath { get; set; }

    [JsonPropertyName("ath_change_percentage")]
    public decimal? AthChangePercentage { get; set; }

    [JsonPropertyName("ath_date")]
    public DateTimeOffset? AthDate { get; set; }

    [JsonPropertyName("atl")]
    public decimal? Atl { get; set; }

    [JsonPropertyName("atl_change_percentage")]
    public decimal? AtlChangePercentage { get; set; }

    [JsonPropertyName("atl_date")]
    public DateTimeOffset? AtlDate { get; set; }

    [JsonPropertyName("roi")]
    public Roi Roi { get; set; }

    [JsonPropertyName("last_updated")]
    public DateTimeOffset? LastUpdated { get; set; }
    
    [JsonPropertyName("sparkline_in_7d")]
    public SparklineIn7D SparklineIn7D { get; set; }
    
    [JsonPropertyName("price_change_percentage_14d_in_currency")]
    public decimal? PriceChangePercentage14DInCurrency { get; set; }

    [JsonPropertyName("price_change_percentage_1h_in_currency")]
    public decimal? PriceChangePercentage1HInCurrency { get; set; }

    [JsonPropertyName("price_change_percentage_1y_in_currency")]
    public decimal? PriceChangePercentage1YInCurrency { get; set; }

    [JsonPropertyName("price_change_percentage_200d_in_currency")]
    public decimal? PriceChangePercentage200DInCurrency { get; set; }

    [JsonPropertyName("price_change_percentage_24h_in_currency")]
    public decimal? PriceChangePercentage24HInCurrency { get; set; }

    [JsonPropertyName("price_change_percentage_30d_in_currency")]
    public decimal? PriceChangePercentage30DInCurrency { get; set; }

    [JsonPropertyName("price_change_percentage_7d_in_currency")]
    public decimal? PriceChangePercentage7DInCurrency { get; set; }
}
