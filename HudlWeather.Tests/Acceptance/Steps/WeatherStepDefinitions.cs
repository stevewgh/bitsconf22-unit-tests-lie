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
    private readonly AngleSharpDriver driver;

    public WeatherStepDefinitions(AngleSharpDriver driver)
    {
        this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
        this.driver.AddTestService<IWeatherGatewayService>(new StubWeatherGateway());
    }

    [When(@"the default page is shown")]
    public async Task WhenTheDefaultPageIsShown()
    {
        await driver.LoadDefaultPage();
    }

    [Then(@"the home weather conditions are displayed")]
    public void ThenTheHomeWeatherConditionsAreDisplayed()
    {
        driver.SelectedLocation.Should().Be("Home");
    }

    [Given(@"the user selected the (.*) option")]
    public async Task GivenAUserSelectedALocation(string location)
    {
        await driver.SelectLocation(location);
    }
    
    [Then(@"the location name (.*) is displayed")]
    public void ThenTheLocationNameIsDisplayed(string locationName)
    {
        driver.SelectedLocationText.Should().EndWith(locationName);
    }
}