namespace CoinGecko.Entities.Response.Derivatives;

using System.Text.Json.Serialization;

public class DerivativesExchangesList
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}