namespace CoinGecko.Entities.Response.Events;

using System.Text.Json.Serialization;

public partial class EventTypes
{
    [JsonPropertyName("data")]
    public string[] Data { get; set; }

    [JsonPropertyName("count")]
    public long? Count { get; set; }
}