namespace Hudl.Weather.Models;

public record WeatherModel(string WeatherImageUrl, string Description);

public record WeatherViewModel(IEnumerable<WeatherModel> WeatherForecasts, string LocationName);