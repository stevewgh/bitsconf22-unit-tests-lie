using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    
    public async Task SelectLocation(string location)
    {
        var formContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("location", location)
        });

        var response = await Client.Value.PostAsync("/", formContent);
        await ParseResponse(response);
    } 
    
    public async Task LoadDefaultPage()
    {
        var defaultPage = await Client.Value.GetAsync("/");

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
    
    private async Task ParseResponse(HttpResponseMessage defaultPage)
    {
        var parser = new HtmlParser();
        var doc = await parser.ParseDocumentAsync(await defaultPage.Content.ReadAsStreamAsync());
        this.Document = doc;
    }
}