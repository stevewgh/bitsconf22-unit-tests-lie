using Hudl.Weather.Models;
using Hudl.Weather.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hudl.Weather.Controllers;

public class HomeController : Controller
{
    private readonly IWeatherGatewayService _weatherGatewayService;

    public HomeController(IWeatherGatewayService weatherGatewayService)
    {
        _weatherGatewayService = weatherGatewayService ?? throw new ArgumentNullException(nameof(weatherGatewayService));
    }

    public async Task<IActionResult> Index()
    {
        SetViewBagLocation(Location.Home);
        return View(await WeatherAtLocation(Location.Home));
    }

    [HttpPost]
    public async Task<IActionResult> Index([FromForm] Location weatherLocation)
    {
        SetViewBagLocation(weatherLocation);
        return View(await WeatherAtLocation(weatherLocation));
    }

    private async Task<WeatherViewModel> WeatherAtLocation(Location location)
    {
        var forecast = await _weatherGatewayService.Forecast(location);
        var weatherImageUri = forecast?.Weather.FirstOrDefault() switch
        {
            { } weather when weather.Main.StartsWith("Cloud") => Url.Content("~/img/cloudy.jpg"),
            _ => Url.Content("~/img/sunny.jpg")
        };
        
        return new WeatherViewModel(weatherImageUri, location.ToString());
    }
    
    private void SetViewBagLocation(Location selectedLocation)
    {
        var values =
            Enum.GetValues(typeof(Location))
                .Cast<Location>();

        var items =
            from value in values
            select new SelectListItem
            {
                Text = value.ToString(),
                Value = value.ToString(),
                Selected = value == selectedLocation,
            };

        ViewBag.Location = items.ToList();
    }
}