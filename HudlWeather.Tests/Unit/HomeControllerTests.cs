using System.Threading.Tasks;
using Hudl.Weather.Controllers;
using Hudl.Weather.Models;
using Hudl.Weather.Services;
using HudlWeather.Tests;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Hudl.Weather.Tests.Unit;

public class HomeControllerTests
{
    [Fact]
    public async Task When_Getting_Index_Then_Home_Is_Default_Location()
    {
        var sut = new HomeController(new WeatherGatewayTestDouble());

        var urlHelper = new Mock<IUrlHelper>();
        urlHelper.Setup(helper => helper.Content(It.IsAny<string?>())).Returns("/doesnt_matter");
        sut.Url = urlHelper.Object;
        
        var result = await sut.Index() as ViewResult;
        var model = result?.Model as WeatherViewModel;
        
        Assert.Equal("Home", model?.LocationName);
    }
    
    [Fact]
    public async Task When_Posting_Location_Then_Location_Is_Set()
    {
        var sut = new HomeController(new WeatherGatewayTestDouble());

        var urlHelper = new Mock<IUrlHelper>();
        urlHelper.Setup(helper => helper.Content(It.IsAny<string?>())).Returns("/doesnt_matter");
        sut.Url = urlHelper.Object;
        
        var result = await sut.Index(Location.Office) as ViewResult;
        var model = result?.Model as WeatherViewModel;
        
        Assert.Equal("Office", model?.LocationName);
    }    
}