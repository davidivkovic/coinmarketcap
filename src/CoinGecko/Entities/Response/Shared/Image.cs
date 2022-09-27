namespace CoinGecko.Entities.Response.Shared;

using System;
using System.Text.Json.Serialization;

public class Image
{
    [JsonPropertyName("thumb")]
    public Uri Thumb { get; set; }

    [JsonPropertyName("small")]
    public Uri Small { get; set; }

    [JsonPropertyName("large")]
    public Uri Large { get; set; }
}