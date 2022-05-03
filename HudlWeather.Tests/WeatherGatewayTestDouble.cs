using System.Threading.Tasks;
using Hudl.Weather.Services;
using Hudl.Weather.Services.WeatherGatewayDto;

namespace HudlWeather.Tests;

public class WeatherGatewayTestDouble : IWeatherGatewayService
{
    public Task<Forecast> Forecast(Location location)
    {
        if (location == Location.Home)
        {
            return Task.FromResult(new Forecast
            {
                Weather = new[] {new Weather() {Main = "Cloudy"}}
            });
        }

        return Task.FromResult(new Forecast
        {
            Weather = new[] {new Weather() {Main = "Sunny"}}
        });
    }
}