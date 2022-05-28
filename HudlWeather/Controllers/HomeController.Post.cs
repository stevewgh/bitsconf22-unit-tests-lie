using Hudl.Weather.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hudl.Weather.Controllers;

public partial class HomeController
{
    [HttpPost]
    public async Task<IActionResult> Index([FromForm] Location location)
    {
        SetViewBagLocation(location);
        var viewModel = await WeatherAtLocation(location);
        return View(viewModel);
    }
}