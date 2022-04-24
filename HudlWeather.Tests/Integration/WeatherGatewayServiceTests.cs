using System.Threading.Tasks;
using HudlWeather.Config;
using HudlWeather.Services;
using Microsoft.Extensions.Options;
using Xunit;

namespace HudlWeather.Tests.Integration;

public class WeatherGatewayServiceTests
{
    [Theory]
    [InlineData(Location.Home, "Birmingham")]
    [InlineData(Location.Office, "Lincoln")]
    public async Task Given_A_Weather_Request_Then_Correct_Name_Is_Returned(Location location, string expectedName)
    {
        IWeatherGatewayService sut = new WeatherGatewayService(new OptionsWrapper<WeatherGatewayOption>(new WeatherGatewayOption()));

        var response = await sut.Forecast(location);
        
        Assert.Equal(expectedName, response.Name);
    }
}