namespace CoinGecko.Entities.Response.Coins;

using System.Text.Json.Serialization;

public class SparklineIn7D
{
    [JsonPropertyName("price")]
    public decimal[] Price { get; set; }
}