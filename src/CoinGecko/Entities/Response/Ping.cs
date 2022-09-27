using System.Text.Json.Serialization;

namespace CoinGecko.Entities.Response
{
    public class Ping
    {
        [JsonPropertyName("gecko_says")]
        public string GeckoSays { get; set; }
    }
}