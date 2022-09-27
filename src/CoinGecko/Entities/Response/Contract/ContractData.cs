namespace CoinGecko.Entities.Response.Contract;

using System;
using System.Collections.Generic;
using CoinGecko.Entities.Response.Coins;
using CoinGecko.Entities.Response.Shared;
using System.Text.Json.Serialization;

public class ContractData
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("asset_platform_id")]
    public string AssetPlatformId { get; set; }

    [JsonPropertyName("block_time_in_minutes")]
    public long? BlockTimeInMinutes { get; set; }

    [JsonPropertyName("hashing_algorithm")]
    public object HashingAlgorithm { get; set; }

    [JsonPropertyName("categories")]
    public object[] Categories { get; set; }

    [JsonPropertyName("localization")]
    public Dictionary<string, string> Localization { get; set; }

    [JsonPropertyName("description")]
    public Dictionary<string, string> Description { get; set; }

    [JsonPropertyName("links")]
    public Links Links { get; set; }

    [JsonPropertyName("image")]
    public Image Image { get; set; }

    [JsonPropertyName("country_origin")]
    public string CountryOrigin { get; set; }

    [JsonPropertyName("genesis_date")]
    public DateTimeOffset? GenesisDate { get; set; }

    [JsonPropertyName("contract_address")]
    public string ContractAddress { get; set; }

    [JsonPropertyName("sentiment_votes_up_percentage")]
    public double? SentimentVotesUpPercentage { get; set; }

    [JsonPropertyName("sentiment_votes_down_percentage")]
    public double? SentimentVotesDownPercentage { get; set; }

    [JsonPropertyName("ico_data")]
    public IcoData IcoData { get; set; }

    [JsonPropertyName("market_cap_rank")] 
    public long? MarketCapRank { get; set; }

    [JsonPropertyName("coingecko_rank")] 
    public long? CoinGeckoRank { get; set; }

    [JsonPropertyName("coingecko_score")]
    public double? CoinGeckoScore { get; set; }

    [JsonPropertyName("developer_score")]
    public double? DeveloperScore { get; set; }

    [JsonPropertyName("community_score")] 
    public double? CommunityScore { get; set; }

    [JsonPropertyName("liquidity_score")] 
    public double? LiquidityScore { get; set; }

    [JsonPropertyName("public_interest_score")] 
    public double? PublicInterestScore { get; set; }

    [JsonPropertyName("market_data")]
    public MarketData MarketData { get; set; }

    [JsonPropertyName("community_data")]
    public CommunityData CommunityData { get; set; }

    [JsonPropertyName("developer_data")]
    public DeveloperData DeveloperData { get; set; }

    [JsonPropertyName("public_interest_stats")]
    public PublicInterestStats PublicInterestStats { get; set; }

    [JsonPropertyName("status_updates")]
    public object[] StatusUpdates { get; set; }

    [JsonPropertyName("last_updated")]
    public DateTimeOffset? LastUpdated { get; set; }

    [JsonPropertyName("tickers")]
    public Ticker[] Tickers { get; set; }
}
public partial class IcoData
{
    [JsonPropertyName("ico_start_date")]
    public DateTimeOffset? IcoStartDate { get; set; }

    [JsonPropertyName("ico_end_date")]
    public DateTimeOffset? IcoEndDate { get; set; }

    [JsonPropertyName("short_desc")]
    public string ShortDesc { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("links")]
    public IcoDataLinks Links { get; set; }

    [JsonPropertyName("softcap_currency")]
    public string SoftcapCurrency { get; set; }

    [JsonPropertyName("hardcap_currency")]
    public string HardcapCurrency { get; set; }

    [JsonPropertyName("total_raised_currency")]
    public string TotalRaisedCurrency { get; set; }

    [JsonPropertyName("softcap_amount")]
    public object SoftcapAmount { get; set; }

    [JsonPropertyName("hardcap_amount")]
    public object HardcapAmount { get; set; }

    [JsonPropertyName("total_raised")]
    public object TotalRaised { get; set; }

    [JsonPropertyName("quote_pre_sale_currency")]
    public string QuotePreSaleCurrency { get; set; }

    [JsonPropertyName("base_pre_sale_amount")]
    public object BasePreSaleAmount { get; set; }

    [JsonPropertyName("quote_pre_sale_amount")]
    public object QuotePreSaleAmount { get; set; }

    [JsonPropertyName("quote_public_sale_currency")]
    public string QuotePublicSaleCurrency { get; set; }

    [JsonPropertyName("base_public_sale_amount")]
    public long? BasePublicSaleAmount { get; set; }

    [JsonPropertyName("quote_public_sale_amount")]
    public long? QuotePublicSaleAmount { get; set; }

    [JsonPropertyName("accepting_currencies")]
    public string AcceptingCurrencies { get; set; }

    [JsonPropertyName("country_origin")]
    public string CountryOrigin { get; set; }

    [JsonPropertyName("pre_sale_start_date")]
    public object PreSaleStartDate { get; set; }

    [JsonPropertyName("pre_sale_end_date")]
    public object PreSaleEndDate { get; set; }

    [JsonPropertyName("whitelist_url")]
    public string WhitelistUrl { get; set; }

    [JsonPropertyName("whitelist_start_date")]
    public object WhitelistStartDate { get; set; }

    [JsonPropertyName("whitelist_end_date")]
    public object WhitelistEndDate { get; set; }

    [JsonPropertyName("bounty_detail_url")]
    public string BountyDetailUrl { get; set; }

    [JsonPropertyName("amount_for_sale")]
    public object AmountForSale { get; set; }

    [JsonPropertyName("kyc_required")]
    public bool KycRequired { get; set; }

    [JsonPropertyName("whitelist_available")]
    public object WhitelistAvailable { get; set; }

    [JsonPropertyName("pre_sale_available")]
    public object PreSaleAvailable { get; set; }

    [JsonPropertyName("pre_sale_ended")]
    public bool PreSaleEnded { get; set; }
}
public partial class IcoDataLinks
{
    [JsonPropertyName("web")]
    public Uri Web { get; set; }

    [JsonPropertyName("blog")]
    public string Blog { get; set; }

    [JsonPropertyName("github")]
    public string Github { get; set; }

    [JsonPropertyName("twitter")]
    public string Twitter { get; set; }

    [JsonPropertyName("facebook")]
    public string Facebook { get; set; }

    [JsonPropertyName("telegram")]
    public string Telegram { get; set; }

    [JsonPropertyName("whitepaper")]
    public Uri Whitepaper { get; set; }
}
