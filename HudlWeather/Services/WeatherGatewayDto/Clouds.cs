using System.Text.Json.Serialization;

namespace Hudl.Weather.Services.WeatherGatewayDto;

public class Clouds
{
    [JsonPropertyName("all")] public long All { get; set; }
}