using Custra.Application.Common.Interfaces;
using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.Persistence;
using Custra.Infrastructure.Authorization;
using Custra.Infrastructure.Identity;
using Custra.Infrastructure.Persistence;
using Custra.Infrastructure.Persistence.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Custra.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<CustraDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        services.AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<CustraDbContext>()
            .AddSignInManager();

        services.AddScoped<IApplicationDbContext>(
            sp => sp.GetRequiredService<CustraDbContext>());

        services.AddScoped<ICustomerQueries, CustomerQueries>();

        services.AddHttpContextAccessor();
        services.AddScoped<
            ICurrentUserService,
            CurrentUserService>();
        services.AddScoped<
            ICurrentOrganizationService,
            CurrentOrganizationService>();

        services.AddScoped<OrganizationContext>();
        services.AddScoped<IOrganizationContext>(sp =>
            sp.GetRequiredService<OrganizationContext>());

        services.AddScoped<IAuthorizationService, AuthorizationService>();

        return services;
    }
}