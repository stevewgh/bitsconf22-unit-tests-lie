using System.Text.Json.Serialization;

namespace Hudl.Weather.Services.WeatherGatewayDto;

public class Forecast
{
    [JsonPropertyName("daily")]
    public Daily[] Daily { get; set; }
}

public class Daily
{
    [JsonPropertyName("weather")]
    public List<Weather> Weather { get; set; }
}

public class Weather
{
    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("icon")]
    public string Icon { get; set; }
}