using System.Text.Json.Serialization;

namespace HudlWeather.Services.WeatherGatewayDto;

public record Forecast
{
    [JsonPropertyName("coord")] public Coord Coord { get; set; }

    [JsonPropertyName("weather")] public Weather[] Weather { get; set; }

    [JsonPropertyName("base")] public string Base { get; set; }

    [JsonPropertyName("main")] public Main Main { get; set; }

    [JsonPropertyName("visibility")] public long Visibility { get; set; }

    [JsonPropertyName("wind")] public Wind Wind { get; set; }

    [JsonPropertyName("clouds")] public Clouds Clouds { get; set; }

    [JsonPropertyName("dt")] public long Dt { get; set; }

    [JsonPropertyName("sys")] public Sys Sys { get; set; }

    [JsonPropertyName("timezone")] public long Timezone { get; set; }

    [JsonPropertyName("id")] public long Id { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("cod")] public long Cod { get; set; }
}