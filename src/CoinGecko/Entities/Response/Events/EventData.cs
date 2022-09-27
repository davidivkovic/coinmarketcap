namespace CoinGecko.Entities.Response.Events;

using System;
using System.Text.Json.Serialization;

public class EventData
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("organizer")]
    public string Organizer { get; set; }

    [JsonPropertyName("start_date")]
    public DateTimeOffset? StartDate { get; set; }

    [JsonPropertyName("end_date")]
    public DateTimeOffset? EndDate { get; set; }

    [JsonPropertyName("website")]
    public Uri Website { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("venue")]
    public string Venue { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("screenshot")]
    public Uri Screenshot { get; set; }
}