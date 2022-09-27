namespace CoinGecko.Entities.Response.Coins;

using System;
using System.Collections.Generic;
using CoinGecko.Entities.Response.Shared;
using System.Text.Json.Serialization;


public class CoinFullData : CoinFullDataWithOutMarketData
{
    [JsonPropertyName("market_data")]
    public MarketData MarketData { get; set; }
}

public class CoinFullDataWithOutMarketData : CoinList
{
    [JsonPropertyName("image")] public Image Image { get; set; }

    [JsonPropertyName("community_data")]
    public CommunityData CommunityData { get; set; }

    [JsonPropertyName("developer_data")]
    public DeveloperData DeveloperData { get; set; }

    [JsonPropertyName("public_interest_stats")]
    public PublicInterestStats PublicInterestStats { get; set; }

    [JsonPropertyName("last_updated")]
    public DateTimeOffset? LastUpdated { get; set; }

    [JsonPropertyName("localization")]
    public Dictionary<string, string> Localization { get; set; }
}

public class DeveloperData
{
    [JsonPropertyName("forks")] 
    public long? Forks { get; set; }

    [JsonPropertyName("stars")] 
    public long? Stars { get; set; }

    [JsonPropertyName("subscribers")] 
    public long? Subscribers { get; set; }

    [JsonPropertyName("total_issues")] 
    public long? TotalIssues { get; set; }

    [JsonPropertyName("closed_issues")] 
    public long? ClosedIssues { get; set; }

    [JsonPropertyName("pull_requests_merged")] 
    public long? PullRequestsMerged { get; set; }

    [JsonPropertyName("pull_request_contributors")]
    public long? PullRequestContributors { get; set; }

    [JsonPropertyName("code_additions_deletions_4_weeks")]
    public Dictionary<string,long?> CodeAdditionsDeletions4Weeks { get; set; }

    [JsonPropertyName("commit_count_4_weeks")] 
    public long? CommitCount4Weeks { get; set; }
    
    [JsonPropertyName("last_4_weeks_commit_activity_series")] 
    public long[] Last4WeeksCommitActivitySeries { get;set; }
}

public class MarketData : MarketDataOhlcv
{
    [JsonPropertyName("roi")] 
    public Roi Roi { get; set; }

    [JsonPropertyName("current_price")] 
    public Dictionary<string, decimal?> CurrentPrice { get; set; }

    [JsonPropertyName("market_cap")] 
    public Dictionary<string, decimal?> MarketCap { get; set; }

    [JsonPropertyName("total_volume")] 
    public Dictionary<string, decimal?> TotalVolume { get; set; }

    [JsonPropertyName("high_24h")] 
    public Dictionary<string, decimal?> High24H { get; set; }

    [JsonPropertyName("low_24h")]
    public Dictionary<string, decimal?> Low24H { get; set; }

    [JsonPropertyName("price_change_percentage_7d")]
    public string PriceChangePercentage7D { get; set; }

    [JsonPropertyName("price_change_percentage_14d")]
    public string PriceChangePercentage14D { get; set; }

    [JsonPropertyName("price_change_percentage_30d")]
    public string PriceChangePercentage30D { get; set; }

    [JsonPropertyName("price_change_percentage_60d")]
    public string PriceChangePercentage60D { get; set; }

    [JsonPropertyName("price_change_percentage_200d")]
    public string PriceChangePercentage200D { get; set; }

    [JsonPropertyName("price_change_percentage_1y")]
    public string PriceChangePercentage1Y { get; set; }

    [JsonPropertyName("price_change_24h_in_currency")]
    public Dictionary<string, decimal> PriceChange24HInCurrency { get; set; }

    [JsonPropertyName("price_change_percentage_1h_in_currency")]
    public Dictionary<string, double> PriceChangePercentage1HInCurrency { get; set; }

    [JsonPropertyName("price_change_percentage_24h_in_currency")]
    public Dictionary<string, double> PriceChangePercentage24HInCurrency { get; set; }

    [JsonPropertyName("price_change_percentage_7d_in_currency")]
    public Dictionary<string, double> PriceChangePercentage7DInCurrency { get; set; }

    [JsonPropertyName("price_change_percentage_14d_in_currency")]
    public Dictionary<string, double> PriceChangePercentage14DInCurrency { get; set; }

    [JsonPropertyName("price_change_percentage_30d_in_currency")]
    public Dictionary<string, double> PriceChangePercentage30DInCurrency { get; set; }

    [JsonPropertyName("price_change_percentage_60d_in_currency")]
    public Dictionary<string, double> PriceChangePercentage60DInCurrency { get; set; }

    [JsonPropertyName("price_change_percentage_200d_in_currency")]
    public Dictionary<string, double> PriceChangePercentage200DInCurrency { get; set; }

    [JsonPropertyName("price_change_percentage_1y_in_currency")]
    public Dictionary<string, double> PriceChangePercentage1YInCurrency { get; set; }

    [JsonPropertyName("market_cap_change_24h_in_currency")]
    public Dictionary<string, decimal> MarketCapChange24HInCurrency { get; set; }

    [JsonPropertyName("market_cap_change_percentage_24h_in_currency")]
    public Dictionary<string, decimal> MarketCapChangePercentage24HInCurrency { get; set; }
}

public class Roi
{
    [JsonPropertyName("times")] 
    public decimal? Times { get; set; }

    [JsonPropertyName("currency")] 
    public string Currency { get; set; }

    [JsonPropertyName("percentage")] 
    public decimal? Percentage { get; set; }
}