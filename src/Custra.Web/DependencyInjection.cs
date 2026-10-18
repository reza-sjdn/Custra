using Custra.Web.Features.Tags.Services;

namespace Custra.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWeb(
        this IServiceCollection services)
    {
        services.AddScoped<ITagAssignmentViewModelService, TagAssignmentViewModelService>();

        return services;
    }
}