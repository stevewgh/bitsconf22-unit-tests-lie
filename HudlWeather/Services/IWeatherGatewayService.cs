namespace Hudl.Weather.Services;

public interface IWeatherGatewayService
{
    Task<IEnumerable<WeatherGatewayDto.Weather>> MultiDayWeatherForecast(Location location);
}