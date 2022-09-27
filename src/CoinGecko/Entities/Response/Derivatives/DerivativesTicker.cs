namespace CoinGecko.Entities.Response.Derivatives;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public class DerivativesTicker
{
    [JsonPropertyName("symbol")] 
    public string Symbol { get; set; }
    
    [JsonPropertyName("base")] 
    public string Base { get; set; }

    [JsonPropertyName("target")] 
    public string Target { get; set; }
    
    [JsonPropertyName("trade_url")] 
    public string TradeUrl { get; set; }
    
    [JsonPropertyName("contract_type")] 
    public string ContractType { get; set; }
    
    [JsonPropertyName("last")] 
    public decimal? Last { get; set; }

    [JsonPropertyName("h24_percentage_change")] 
    public decimal? PricePctChange { get; set; }

    [JsonPropertyName("index")] 
    public decimal? Index { get; set; }
    
    [JsonPropertyName("index_basis_percentage")] 
    public decimal? IndexBasisPercentage { get; set; }
    
    [JsonPropertyName("bid_ask_spread")] 
    public decimal? BidAskSpread { get; set; }
    
    [JsonPropertyName("funding_rate")] 
    public decimal? FundingRate { get; set; }
    
    [JsonPropertyName("open_interest_usd")] 
    public decimal? OpenInterestUsd { get; set; }
    
    [JsonPropertyName("converted_volume")] 
    public Dictionary<string, decimal> ConvertedVolume { get; set; }
    
    [JsonPropertyName("converted_last")] 
    public Dictionary<string, decimal> ConvertedLast { get; set; }
    
    [JsonPropertyName("last_traded")] 
    public int? LastTraded { get; set; }
    
    [JsonPropertyName("expired_at")] 
    public int? ExpiredAt { get; set; }
}