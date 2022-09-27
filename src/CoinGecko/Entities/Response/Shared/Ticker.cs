namespace CoinGecko.Entities.Response.Shared;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Ticker
{
    [JsonPropertyName("base")] 
    public string Base { get; set; }

    [JsonPropertyName("target")] 
    public string Target { get; set; }

    [JsonPropertyName("market")] 
    public Market Market { get; set; }

    [JsonPropertyName("last")]
    public decimal? Last { get; set; }

    [JsonPropertyName("cost_to_move_up_usd")] 
    public decimal CostToMoveUpUsd { get; set; }

    [JsonPropertyName("cost_to_move_down_usd")] 
    public decimal CostToMoveDownUsd { get; set; }

    [JsonPropertyName("converted_last")]
    public Dictionary<string, decimal> ConvertedLast { get; set; }

    [JsonPropertyName("volume")] 
    public decimal? Volume { get; set; }

    [JsonPropertyName("converted_volume")] 
    public Dictionary<string, decimal> ConvertedVolume { get; set; }

    [JsonPropertyName("trust_score")] 
    public string TrustScore { get; set; }

    [JsonPropertyName("timestamp")] 
    public DateTimeOffset? Timestamp { get; set; }

    [JsonPropertyName("last_traded_at")] 
    public DateTimeOffset? LastTradedAt { get; set; }

    [JsonPropertyName("last_fetch_at")] 
    public DateTimeOffset? LastFetchAt { get; set; }

    [JsonPropertyName("is_anomaly")] 
    public bool IsAnomaly { get; set; }

    [JsonPropertyName("is_stale")]
    public bool IsStale { get; set; }

    [JsonPropertyName("trade_url")] 
    public string TradeUrl { get; set; }

    [JsonPropertyName("coin_id")] 
    public string CoinId { get; set; }
    [JsonPropertyName("target_coin_id")] 
    public string TargetCoinId { get; set; }
}