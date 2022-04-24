using System.Text.Json.Serialization;

namespace HudlWeather.Services.WeatherGatewayDto;

public class Clouds
{
    [JsonPropertyName("all")] public long All { get; set; }
}