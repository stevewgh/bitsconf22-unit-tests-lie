using System.Threading.Tasks;
using Hudl.Weather.Services;
using Hudl.Weather.Services.WeatherGatewayDto;

namespace Hudl.Weather.Tests;

public class StubWeatherGateway : IWeatherGatewayService
{
    public Task<Forecast> Forecast(Location location)
    {
        if (location == Location.Home)
        {
            return Task.FromResult(new Forecast
            {
                Weather = new[] {new Services.WeatherGatewayDto.Weather() {Main = "Cloudy"}}
            });
        }

        return Task.FromResult(new Forecast
        {
            Weather = new[] {new Services.WeatherGatewayDto.Weather() {Main = "Sunny"}}
        });
    }
}