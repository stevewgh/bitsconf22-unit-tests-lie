using System.Threading.Tasks;
using HudlWeather.Controllers;
using HudlWeather.Models;
using HudlWeather.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace HudlWeather.Tests.Unit;

public class HomeControllerTests
{
    [Fact]
    public void When_Getting_Index_Then_Home_Is_Default_Location()
    {
        var sut = new HomeController(Mock.Of<IWeatherGatewayService>());

        var urlHelper = new Mock<IUrlHelper>();
        urlHelper.Setup(helper => helper.Content(It.IsAny<string?>())).Returns("test");
        sut.Url = urlHelper.Object;
        
        var result = sut.Index() as ViewResult;
        var model = result?.Model as WeatherViewModel;
        
        Assert.Equal("Home", model?.LocationName);
    }
    
    [Fact]
    public void When_Posting_Location_Then_Location_Is_Set()
    {
        var sut = new HomeController(Mock.Of<IWeatherGatewayService>());

        var urlHelper = new Mock<IUrlHelper>();
        urlHelper.Setup(helper => helper.Content(It.IsAny<string?>())).Returns("test");
        sut.Url = urlHelper.Object;
        
        var result = sut.Index(Location.Office) as ViewResult;
        var model = result?.Model as WeatherViewModel;
        
        Assert.Equal("Office", model?.LocationName);
    }    
}