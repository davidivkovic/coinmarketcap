namespace CoinGecko.Entities.Response.Exchanges;

using System.Text.Json.Serialization;
using CoinGecko.Entities.Response.Shared;

public class ExchangeById : Exchanges
{
    [JsonPropertyName("tickers")]
    public Ticker[] Tickers { get; set; }

    [JsonPropertyName("status_updates")]
    public object[] StatusUpdates { get; set; }
    
    [JsonPropertyName("facebook_url")]
    public string FacebookUrl { get; set; }
    
    [JsonPropertyName("reddit_url")]
    public string RedditUrl { get; set; }
    
    [JsonPropertyName("telegram_url")]
    public string TelegramUrl { get; set; }
    
    [JsonPropertyName("slack_url")]
    public string SlackUrl { get; set; }
    
    [JsonPropertyName("other_url_1")]
    public string Other1Url { get; set; }
    
    [JsonPropertyName("other_url_2")]
    public string Other2Url { get; set; }
    
    [JsonPropertyName("twitter_handle")]
    public string TwitterHandle { get; set; }
    
    [JsonPropertyName("centralized")]
    public bool? Centralized { get; set; }
    
    [JsonPropertyName("public_notice")]
    public string PublicNotice { get; set; }
    
    [JsonPropertyName("alert_notice")]
    public string AlertNotice { get; set; }
}