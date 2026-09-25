using Microsoft.AspNetCore.Mvc.Razor;

namespace Custra.Web.Infrastructure.Mvc;

public sealed class FeatureViewLocationExpander : IViewLocationExpander
{
    public IEnumerable<string> ExpandViewLocations(
        ViewLocationExpanderContext context,
        IEnumerable<string> viewLocations)
    {
        var locations = new[]
        {
            "/Features/{1}/Views/{0}.cshtml",
            "/Features/{1}/Views/Shared/{0}.cshtml"
        };

        return locations.Concat(viewLocations);
    }

    public void PopulateValues(
        ViewLocationExpanderContext context)
    {
    }
}