namespace CoinGecko.Entities.Response.Indexes;

using System.Text.Json.Serialization;

public class IndexList
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}