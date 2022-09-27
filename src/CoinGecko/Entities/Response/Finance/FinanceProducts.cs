namespace CoinGecko.Entities.Response.Finance;

using System.Text.Json.Serialization;

public class FinanceProducts
{
    [JsonPropertyName("platform")]
    public string Platform { get; set; }

    [JsonPropertyName("identifier")]
    public string Identifier { get; set; }

    [JsonPropertyName("supply_rate_percentage")]
    public string SupplyRatePercentage { get; set; }

    [JsonPropertyName("borrow_rate_percentage")]
    public string BorrowRatePercentage { get; set; }

    [JsonPropertyName("number_duration")]
    public object NumberDuration { get; set; }

    [JsonPropertyName("length_duration")]
    public object LengthDuration { get; set; }

    [JsonPropertyName("start_at")]
    public long? StartAt { get; set; }

    [JsonPropertyName("end_at")]
    public long? EndAt { get; set; }

    [JsonPropertyName("value_at")]
    public long? ValueAt { get; set; }

    [JsonPropertyName("redeem_at")]
    public long? RedeemAt { get; set; }
}