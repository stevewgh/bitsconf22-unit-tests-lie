using Hudl.Weather.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hudl.Weather.Controllers;

public partial class HomeController
{
    [HttpPost]
    public async Task<IActionResult> Index([FromForm] Location weatherLocation)
    {
        SetViewBagLocation(weatherLocation);
        var viewModel = await WeatherAtLocation(weatherLocation);
        return View(viewModel);
    }
}