using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;

namespace Hudl.Weather.Tests.Acceptance.Driver;

public class AngleSharpDriver
{
    private readonly ScenarioContext scenarioContext;
    private Lazy<HttpClient> Client { get; }

    private IHtmlDocument? Document { get; set; }

    public AngleSharpDriver(ScenarioContext scenarioContext, Lazy<HttpClient> client)
    {
        this.scenarioContext = scenarioContext ?? throw new ArgumentNullException(nameof(scenarioContext));
        Client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public string? SelectedLocation
    {
        get
        {
            if (Document?.GetElementById("Location") is not IHtmlSelectElement location)
            {
                throw new NullReferenceException("location select element was not found.");
            }

            return location.SelectedOptions.FirstOrDefault()?.Value;
        }
    }

    public string? SelectedLocationText
    {
        get
        {
            if (Document?.GetElementById("locationName") is not IHtmlHeadingElement locationNameElement)
            {
                throw new NullReferenceException("location name was not found.");
            }

            return locationNameElement.InnerHtml;
        }
    }

    public async Task SendAntiochRequest(FileInfo fileLocation)
    {
        var file = File.OpenRead(fileLocation.FullName);
        var document = await JsonDocument.ParseAsync(file);

        var method = document.RootElement.GetProperty("request").GetProperty("method").GetString();
        var endpoint = document.RootElement.GetProperty("request").GetProperty("endpoint").GetString();
        var payload = document.RootElement.GetProperty("request").GetProperty("payload").GetRawText();

        var response = await this.Client.Value.PostAsync(endpoint, new StringContent(payload));
        response.EnsureSuccessStatusCode();
        await ParseResponse(response);
    }
    
    public async Task SelectLocation(string location)
    {
        var formContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("location", location)
        });

        var response = await Client.Value.PostAsync("/", formContent);
        response.EnsureSuccessStatusCode();
        await ParseResponse(response);
    }

    public async Task LoadDefaultPage()
    {
        var defaultPage = await Client.Value.GetAsync("/");
        defaultPage.EnsureSuccessStatusCode();
        await ParseResponse(defaultPage);        
    }

    public void AddTestService<T>(T service) where T : class
    {
        void ServiceRegistration(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(service);
        }

        this.scenarioContext.ScenarioContainer.RegisterInstanceAs((Action<IServiceCollection>) ServiceRegistration);
    }

    private async Task ParseResponse(HttpResponseMessage response)
    {
        var parser = new HtmlParser();
        var stream = await response.Content.ReadAsStreamAsync();
        var doc = await parser.ParseDocumentAsync(stream);
        this.Document = doc;
    }
}