using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HudlWeather.Models;
using HudlWeather.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HudlWeather.Controllers;

public class HomeController : Controller
{
    private readonly IWeatherGatewayService _weatherGatewayService;

    public HomeController(IWeatherGatewayService weatherGatewayService)
    {
        _weatherGatewayService =
            weatherGatewayService ?? throw new ArgumentNullException(nameof(weatherGatewayService));
    }

    public IActionResult Index()
    {
        var cloudy = this.Url.Content("~/img/cloudy.jpg");
        SetViewBagLocation(Location.Home);
        return View(new WeatherViewModel(cloudy, "Home"));
    }

    [HttpPost]
    public IActionResult Index([FromForm] Location weatherLocation)
    {
        var cloudy = this.Url.Content("~/img/cloudy.jpg");
        var sunny = this.Url.Content("~/img/sunny.jpg");
        
        SetViewBagLocation(weatherLocation);

        return View(new WeatherViewModel(cloudy, weatherLocation.ToString()));
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

        ViewBag.Location = items;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}