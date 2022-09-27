namespace CoinGecko.Entities.Response.Shared;

using System;
using System.Text.Json.Serialization;
using CoinGecko.Entities.Response.Coins;

public class Links
{
    [JsonPropertyName("homepage")] 
    public string[] Homepage { get; set; }

    [JsonPropertyName("blockchain_site")] 
    public string[] BlockchainSite { get; set; }

    [JsonPropertyName("official_forum_url")] 
    public string[] OfficialForumUrl { get; set; }

    [JsonPropertyName("chat_url")] 
    public string[] ChatUrl { get; set; }

    [JsonPropertyName("announcement_url")] 
    public string[] AnnouncementUrl { get; set; }

    [JsonPropertyName("twitter_screen_name")] 
    public string TwitterScreenName { get; set; }

    [JsonPropertyName("facebook_username")] 
    public string FacebookUsername { get; set; }

    [JsonPropertyName("bitcointalk_thread_identifier")]
    public object BitcointalkThreadIdentifier { get; set; }

    [JsonPropertyName("telegram_channel_identifier")]
    public string TelegramChannelIdentifier { get; set; }

    [JsonPropertyName("subreddit_url")] 
    public Uri SubredditUrl { get; set; }

    [JsonPropertyName("repos_url")] 
    public ReposUrl ReposUrl { get; set; }
}