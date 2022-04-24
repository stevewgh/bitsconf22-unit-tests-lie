using HudlWeather.Config;
using HudlWeather.Services.WeatherGatewayDto;
using Microsoft.Extensions.Options;

namespace HudlWeather.Services;

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