using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;

namespace Hudl.Weather.Tests.Acceptance.Hooks;

[Binding]
public class ScenarioHooks
{
    private readonly ScenarioContext scenarioContext;

    public ScenarioHooks(ScenarioContext scenarioContext)
    {
        this.scenarioContext = scenarioContext ?? throw new ArgumentNullException(nameof(scenarioContext));
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

        scenarioContext.ScenarioContainer.RegisterInstanceAs(
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
                if (scenarioContext.ScenarioContainer.IsRegistered<Action<IServiceCollection>>())
                {
                    var testServicesBuilder = scenarioContext.ScenarioContainer.Resolve<Action<IServiceCollection>>();
                    builder.ConfigureTestServices(testServicesBuilder);
                }
            });

        return application.CreateClient();
    }
}