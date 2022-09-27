namespace CoinGecko.Entities.Response.StatusUpdates;

using System;
using System.Text.Json.Serialization;
using CoinGecko.Entities.Response.Shared;

public partial class StatusUpdate
{
    [JsonPropertyName("status_updates")]
    public StatusUpdateElement[] StatusUpdates { get; set; }
}

public partial class StatusUpdateElement
{
    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("category")]
    public string Category { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonPropertyName("user")]
    public string User { get; set; }

    [JsonPropertyName("user_title")]
    public string UserTitle { get; set; }

    [JsonPropertyName("pin")]
    public bool Pin { get; set; }

    [JsonPropertyName("project")]
    public Project Project { get; set; }
}

public partial class Project
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; }

    [JsonPropertyName("image")]
    public Image Image { get; set; }
}