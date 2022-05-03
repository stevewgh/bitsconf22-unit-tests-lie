using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;

namespace HudlWeather.Tests.Acceptance.Hooks;

[Binding]
public class ScenarioHooks
{
    private readonly ScenarioContext _scenarioContext;

    public ScenarioHooks(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext ?? throw new ArgumentNullException(nameof(scenarioContext));
    }
    
    [BeforeScenario]
    public void InjectConfiguration()
    {
        var builder = new ConfigurationBuilder();
        builder.AddJsonFile("./Acceptance/appsettings.json", false);
        builder.AddEnvironmentVariables();
        builder.Build();
        var configuration = builder.Build();

        var hostAddress = configuration["HostAddress"];

        _scenarioContext.ScenarioContainer.RegisterInstanceAs(
            new Lazy<HttpClient>(() => 
                hostAddress switch
                {
                    "http://localhost" => CreateClientForInProcessServer(),
                    _ => CreateClientForOutOfProcessServer(hostAddress)
                }
            )
        );
    }

    private static HttpClient CreateClientForOutOfProcessServer(string hostAddress)
    {
        return new HttpClient
        {
            BaseAddress = new Uri(hostAddress)
        };
    }

    private HttpClient CreateClientForInProcessServer()
    {
        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                if (_scenarioContext.ScenarioContainer.IsRegistered<Action<IServiceCollection>>())
                {
                    var testServicesBuilder = _scenarioContext.ScenarioContainer.Resolve<Action<IServiceCollection>>();
                    builder.ConfigureTestServices(testServicesBuilder);
                }
            });

        return application.CreateClient();
    }
}