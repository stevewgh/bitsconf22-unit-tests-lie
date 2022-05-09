﻿using Hudl.Weather.Models;
using Hudl.Weather.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Location = Hudl.Weather.Services.Location;

namespace Hudl.Weather.Controllers;

public class HomeController : Controller
{
    private readonly IWeatherGatewayService weatherGatewayService;

    public HomeController(IWeatherGatewayService weatherGatewayService)
    {
        this.weatherGatewayService = weatherGatewayService ?? throw new ArgumentNullException(nameof(weatherGatewayService));
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
        var forecast = await weatherGatewayService.MultiDayWeatherForecast(location);

        var forecasts = forecast.Select(weather =>
            new WeatherModel(Url.Content($@"~/img/{weather.Icon}@4x.png"), weather.Description));
        
        return new WeatherViewModel(forecasts, location.ToString());
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