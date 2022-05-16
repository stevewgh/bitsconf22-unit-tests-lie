using Hudl.Weather.Config;
using Hudl.Weather.Services.WeatherGatewayDto;
using Microsoft.Extensions.Options;

namespace Hudl.Weather.Services;

public class WeatherGatewayService : IWeatherGatewayService
{
    private record CoOrd(double Lat, double Lon);
    private readonly IOptions<WeatherGatewayOption> options;
    private readonly HttpClient client;
    private readonly Dictionary<Location, CoOrd> locationCoordinates = new()
    {
        {
            Location.Home,
            new CoOrd(52.486244,-1.890401)
        },
        {
            Location.Office,
            new CoOrd(40.8136,-96.681679)
        },
        {
            Location.Vacation,
            new CoOrd(30.3968286,-9.5970171)
        }
    };

    public WeatherGatewayService(IOptions<WeatherGatewayOption> options)
    {
        this.options = options ?? throw new ArgumentNullException(nameof(options));
        client = new HttpClient()
        {
            BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/")
        };
    }

    public async Task<IEnumerable<WeatherGatewayDto.Weather>> MultiDayWeatherForecast(Location location)
    {
        var uri =
            $"onecall?lat={locationCoordinates[location].Lat}&lon={locationCoordinates[location].Lon}&appid={options.Value.ApiKey}&exclude=current,minutely,hourly,alerts";

        var fromJsonAsync = (await client.GetFromJsonAsync<Forecast>(uri));
        return
            fromJsonAsync?.Daily?.SelectMany(daily => daily.Weather) ??
            throw new InvalidOperationException();
    }
}