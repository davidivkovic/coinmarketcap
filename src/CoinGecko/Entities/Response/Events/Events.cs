namespace CoinGecko.Entities.Response.Events;

using System.Text.Json.Serialization;

public partial class Events
{
    [JsonPropertyName("data")]
    public EventData[] Data { get; set; }

    [JsonPropertyName("count")]
    public long? Count { get; set; }

    [JsonPropertyName("page")]
    public long? Page { get; set; }
}