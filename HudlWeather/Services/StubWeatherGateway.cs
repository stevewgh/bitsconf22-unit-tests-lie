namespace Hudl.Weather.Services;

public class StubWeatherGateway : IWeatherGatewayService
{
    public Task<IEnumerable<Services.WeatherGatewayDto.Weather>> MultiDayWeatherForecast(Location location)
    {
        var conditionsSuperset = new List<Services.WeatherGatewayDto.Weather>()
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
        
        var conditions = new List<WeatherGatewayDto.Weather>();
        for (var i = 0; i < 8; i++)
        {
            var index = Random.Shared.Next(0, conditionsSuperset.Count - 1);
            conditions.Add(conditionsSuperset[index]);
        }
        
        return Task.FromResult((IEnumerable<Services.WeatherGatewayDto.Weather>)conditions);
    }
}