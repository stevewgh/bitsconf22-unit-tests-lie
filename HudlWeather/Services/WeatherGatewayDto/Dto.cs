using System.Text.Json.Serialization;
// ReSharper disable ClassNeverInstantiated.Global

namespace Hudl.Weather.Services.WeatherGatewayDto;

public class Forecast
{
    [JsonPropertyName("daily")]
    public IEnumerable<Daily> Daily { get; set; } = null!;
}

public class Daily
{
    [JsonPropertyName("weather")]
    public IEnumerable<Weather> Weather { get; set; } = null!;
}

public class Weather
{
    [JsonPropertyName("description")]
    public string Description { get; set; } = null!;

    [JsonPropertyName("icon")]
    public string Icon { get; set; } = null!;
}