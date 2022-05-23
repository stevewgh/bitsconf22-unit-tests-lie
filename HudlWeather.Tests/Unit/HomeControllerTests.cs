using System.Threading.Tasks;
using FluentAssertions;
using Hudl.Weather.Controllers;
using Hudl.Weather.Models;
using Hudl.Weather.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Hudl.Weather.Tests.Unit;

public class HomeControllerTests
{
    [Theory]
    [InlineData(Location.Home, "Home")]
    [InlineData(Location.Office, "Office")]
    [InlineData(Location.Vacation, "Vacation")]
    public async Task When_Location_Changes_Then_Model_Is_Correct_Location(Location location, string expectedLocationName)
    {
        //  arrange
        var sut = BuildController();

        //  act
        var result = await sut.Index(location) as ViewResult;

        //  assert
        var model = result?.Model as WeatherViewModel;
        model!.LocationName.Should().Be(expectedLocationName);
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