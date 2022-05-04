using System.Threading.Tasks;
using Hudl.Weather.Controllers;
using Hudl.Weather.Models;
using Hudl.Weather.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Hudl.Weather.Tests.Unit;

public class HomeControllerTests
{
    [Fact]
    public async Task When_Getting_Index_Then_Home_Is_Default_Location()
    {
        //  arrange
        var sut = BuildController();

        //  act
        var result = await sut.Index() as ViewResult;

        //  assert
        var model = result?.Model as WeatherViewModel;
        Assert.Equal("Home", model?.LocationName);
    }

    [Fact]
    public async Task When_Location_Changes_Then_Model_Is_Correct_Location()
    {
        //  arrange
        var sut = BuildController();

        //  act
        var result = await sut.Index(Location.Office) as ViewResult;

        //  assert
        var model = result?.Model as WeatherViewModel;
        Assert.Equal("Office", model?.LocationName);
    }

    private static HomeController BuildController()
    {
        var weatherGatewayService = new StubWeatherGateway();

        return
            new HomeController(weatherGatewayService)
            {
                Url = new FakeUrlHelper()
            };
    }
}