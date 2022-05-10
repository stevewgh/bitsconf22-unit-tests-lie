using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Hudl.Weather.Tests;

public class FakeUrlHelper : IUrlHelper
{
    public string? Content(string? contentPath)
    {
        return "/doesnt_matter";
    }

    public string? Action(UrlActionContext actionContext)
    {
        throw new System.NotImplementedException();
    }

    public bool IsLocalUrl(string? url)
    {
        throw new System.NotImplementedException();
    }

    public string? RouteUrl(UrlRouteContext routeContext)
    {
        throw new System.NotImplementedException();
    }

    public string? Link(string? routeName, object? values)
    {
        throw new System.NotImplementedException();
    }

    public ActionContext ActionContext { get; } = new();
}