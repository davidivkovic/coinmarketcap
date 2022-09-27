namespace CoinGecko.Entities.Response.Exchanges;

using System.Text.Json.Serialization;

public class ExchangesList
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}