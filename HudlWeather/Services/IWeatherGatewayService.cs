using Hudl.Weather.Services.WeatherGatewayDto;

namespace Hudl.Weather.Services;

public interface IWeatherGatewayService
{
    Task<Forecast> Forecast(Location location);
}