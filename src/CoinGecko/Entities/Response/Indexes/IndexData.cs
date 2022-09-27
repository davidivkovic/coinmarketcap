namespace CoinGecko.Entities.Response.Indexes;

using System.Text.Json.Serialization;

public class IndexData
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("market")]
    public string Market { get; set; }

    [JsonPropertyName("last")]
    public double? Last { get; set; }

    [JsonPropertyName("is_multi_asset_composite")]
    public bool? IsMultiAssetComposite { get; set; }
}