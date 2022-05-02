using System;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Microsoft.AspNetCore.Mvc.Testing;
using TechTalk.SpecFlow;

namespace HudlWeather.Tests.Acceptance.Context;

public class WeatherContext
{
    public ScenarioContext ScenarioContext { get; }

    public WeatherContext(ScenarioContext context)
    {
        ScenarioContext = context ?? throw new ArgumentNullException(nameof(context));
        this.Client = BuildClient();
    }

    public HttpClient Client { get; }

    public IHtmlDocument Document { get; private set; }

    private HttpClient BuildClient()
    {
        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                // ... Configure test services
            });

        var client = application.CreateClient();
        return client;
    }

    public async Task ParseResponse(HttpResponseMessage defaultPage)
    {
        var parser = new HtmlParser();
        var doc = await parser.ParseDocumentAsync(await defaultPage.Content.ReadAsStreamAsync());
        this.Document = doc;
    }
}