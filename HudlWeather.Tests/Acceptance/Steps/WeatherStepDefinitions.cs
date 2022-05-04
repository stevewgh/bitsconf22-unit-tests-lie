using System;
using System.Threading.Tasks;
using FluentAssertions;
using Hudl.Weather.Services;
using Hudl.Weather.Tests.Acceptance.Driver;
using TechTalk.SpecFlow;

namespace Hudl.Weather.Tests.Acceptance.Steps;

[Binding]
public class WeatherStepDefinitions
{
    private readonly AngleSharpDriver _driver;

    public WeatherStepDefinitions(AngleSharpDriver driver)
    {
        _driver = driver ?? throw new ArgumentNullException(nameof(driver));
        _driver.AddTestService<IWeatherGatewayService>(new StubWeatherGateway());
    }

    [When(@"the default page is shown")]
    public async Task WhenTheDefaultPageIsShown()
    {
        await _driver.LoadDefaultPage();
    }

    [Then(@"the home weather conditions are displayed")]
    public void ThenTheHomeWeatherConditionsAreDisplayed()
    {
        _driver.SelectedLocation.Should().Be("Home");
    }

    [Given(@"the user selected the (.*) location")]
    public async Task GivenAUserSelectedALocation(string location)
    {
        await _driver.SelectLocation(location);
    }
    
    [Then(@"the location name (.*) is displayed")]
    public void ThenTheLocationNameIsDisplayed(string locationName)
    {
        _driver.SelectedLocationText.Should().Be(locationName);
    }
}