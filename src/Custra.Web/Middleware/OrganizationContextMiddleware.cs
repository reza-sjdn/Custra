using Custra.Application.Common.Interfaces;
using Custra.Infrastructure.Identity;

public sealed class OrganizationContextMiddleware
{
    private readonly RequestDelegate _next;

    public OrganizationContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context,
        ICurrentOrganizationService currentOrganizationService,
        OrganizationContext organizationContext)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var organizationId =
                await currentOrganizationService.GetOrganizationIdAsync();

            organizationContext.SetOrganization(organizationId);
        }

        await _next(context);
    }
}