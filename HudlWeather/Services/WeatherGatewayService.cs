using System.Text.Json.Serialization;
using HudlWeather.Config;
using Microsoft.Extensions.Options;

namespace HudlWeather.Services;

public enum Location
{
    Home,
    Office
}

public interface IWeatherGatewayService
{
    Task<Forecast> Forecast(Location location);
}

public class WeatherGatewayService : IWeatherGatewayService
{
    private readonly IOptions<WeatherGatewayOption> options;
    private readonly HttpClient client;

    public WeatherGatewayService(IOptions<WeatherGatewayOption> options)
    {
        this.options = options ?? throw new ArgumentNullException(nameof(options));
        client = new HttpClient()
        {
            BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/weather")
        };
    }
    
    public async Task<Forecast> Forecast(Location location)
    {
        var locationCoordinates = new Dictionary<Location, Coord>()
        {
            {
                Location.Home,
                new Coord
                {
                    Lat = 52.486244,
                    Lon = -1.890401
                }
            },
            {
                Location.Office,
                new Coord
                {
                    Lat = 40.8136,
                    Lon = -96.681679
                }
            }
        };

        var uri = $"?lat={locationCoordinates[location].Lat}&lon={locationCoordinates[location].Lon}&appid={options.Value.ApiKey}";

        return new Forecast();
        // return 
        //     await client.GetFromJsonAsync<Forecast>(uri) ?? throw new InvalidOperationException();
    }
}

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

public class Clouds
{
    [JsonPropertyName("all")] public long All { get; set; }
}

public class Coord
{
    [JsonPropertyName("lon")] public double Lon { get; set; }

    [JsonPropertyName("lat")] public double Lat { get; set; }
}

public class Main
{
    [JsonPropertyName("temp")] public double Temp { get; set; }

    [JsonPropertyName("feels_like")] public double FeelsLike { get; set; }

    [JsonPropertyName("temp_min")] public double TempMin { get; set; }

    [JsonPropertyName("temp_max")] public double TempMax { get; set; }

    [JsonPropertyName("pressure")] public long Pressure { get; set; }

    [JsonPropertyName("humidity")] public long Humidity { get; set; }
}

public class Sys
{
    [JsonPropertyName("type")] public long Type { get; set; }

    [JsonPropertyName("id")] public long Id { get; set; }

    [JsonPropertyName("message")] public double Message { get; set; }

    [JsonPropertyName("country")] public string Country { get; set; }

    [JsonPropertyName("sunrise")] public long Sunrise { get; set; }

    [JsonPropertyName("sunset")] public long Sunset { get; set; }
}

public class Weather
{
    [JsonPropertyName("id")] public long Id { get; set; }

    [JsonPropertyName("main")] public string Main { get; set; }

    [JsonPropertyName("description")] public string Description { get; set; }

    [JsonPropertyName("icon")] public string Icon { get; set; }
}

public class Wind
{
    [JsonPropertyName("speed")] public double Speed { get; set; }

    [JsonPropertyName("deg")] public long Deg { get; set; }
}