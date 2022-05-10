namespace Hudl.Weather.Services;

public class StubWeatherGateway : IWeatherGatewayService
{
    public Task<IEnumerable<Services.WeatherGatewayDto.Weather>> MultiDayWeatherForecast(Location location)
    {
        IEnumerable<Services.WeatherGatewayDto.Weather> weatherConditions =
            new List<Services.WeatherGatewayDto.Weather>()
            {
                new() {Description = "Raining", Icon = "09d"},
                new() {Description = "Clear", Icon = "01d"},
                new() {Description = "Sunny", Icon = "01d"},
                new() {Description = "Sunny", Icon = "01d"},
                new() {Description = "Sunny", Icon = "01d"},
                new() {Description = "Sunny", Icon = "01d"},
                new() {Description = "Snowing", Icon ="13d" },
                new() {Description = "Misty", Icon = "50d"},
            };
        
        return Task.FromResult( weatherConditions);
    }
}