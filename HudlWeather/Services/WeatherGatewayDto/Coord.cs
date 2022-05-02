using System.Text.Json.Serialization;

namespace Hudl.Weather.Services.WeatherGatewayDto;

public class Coord
{
    [JsonPropertyName("lon")] public double Lon { get; set; }

    [JsonPropertyName("lat")] public double Lat { get; set; }
}