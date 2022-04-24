using System.Text.Json.Serialization;

namespace HudlWeather.Services.WeatherGatewayDto;

public class Wind
{
    [JsonPropertyName("speed")] public double Speed { get; set; }

    [JsonPropertyName("deg")] public long Deg { get; set; }
}