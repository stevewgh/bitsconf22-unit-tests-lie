using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using FluentAssertions;
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

    [Given(@"I run the test ""(.*)""")]
    public async Task ThenIRunTheTest(string testPath)
    {
        var currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        await driver.SendAntiochRequest(new FileInfo(Path.Combine(currentPath!, "Acceptance", testPath)));
    }
}