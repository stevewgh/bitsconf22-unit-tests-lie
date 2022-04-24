using HudlWeather.Services.WeatherGatewayDto;

namespace HudlWeather.Services;

public interface IWeatherGatewayService
{
    Task<Forecast> Forecast(Location location);
}