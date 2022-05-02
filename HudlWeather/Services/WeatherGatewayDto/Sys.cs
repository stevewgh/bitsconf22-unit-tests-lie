using System.Text.Json.Serialization;

namespace Hudl.Weather.Services.WeatherGatewayDto;

public class Sys
{
    [JsonPropertyName("type")] public long Type { get; set; }

    [JsonPropertyName("id")] public long Id { get; set; }

    [JsonPropertyName("message")] public double Message { get; set; }

    [JsonPropertyName("country")] public string Country { get; set; }

    [JsonPropertyName("sunrise")] public long Sunrise { get; set; }

    [JsonPropertyName("sunset")] public long Sunset { get; set; }
}