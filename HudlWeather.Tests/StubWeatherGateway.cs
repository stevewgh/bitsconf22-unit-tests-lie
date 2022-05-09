using System.Collections.Generic;
using System.Threading.Tasks;
using Hudl.Weather.Services;
using Location = Hudl.Weather.Services.Location;

namespace Hudl.Weather.Tests;

public class StubWeatherGateway : IWeatherGatewayService
{
    public Task<IEnumerable<Services.WeatherGatewayDto.Weather>> MultiDayWeatherForecast(Location location)
    {
        IEnumerable<Services.WeatherGatewayDto.Weather> weatherConditions =
            new List<Services.WeatherGatewayDto.Weather>()
            {
                new() {Description = "Raining", Icon = "rain.png"},
                new() {Description = "Clear", Icon = "clear.png"},
                new() {Description = "Sunny", Icon = "sunny.png"},
                new() {Description = "Snowing", Icon = "snow.png"},
                new() {Description = "Misty", Icon = "misty.png"},
            };
        
        return Task.FromResult( weatherConditions);
    }
}