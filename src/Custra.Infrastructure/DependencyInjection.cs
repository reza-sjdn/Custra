using Custra.Application.Common.Interfaces;
using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Infrastructure.Authorization;
using Custra.Infrastructure.Identity;
using Custra.Infrastructure.Persistence;
using Custra.Infrastructure.Persistence.Commands;
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
        services.AddScoped<ICustomerCommands, CustomerCommands>();


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

        services.AddScoped<IContactQueries, ContactQueries>();
        services.AddScoped<IContactCommands, ContactCommands>();

        services.AddScoped<ILeadQueries, LeadQueries>();
        services.AddScoped<ILeadCommands, LeadCommands>();

        services.AddScoped<ISalesPipelineQueries, SalesPipelineQueries>();
        services.AddScoped<ISalesPipelineCommands, SalesPipelineCommands>();

        services.AddScoped<IOpportunityQueries, OpportunityQueries>();
        services.AddScoped<IOpportunityCommands, OpportunityCommands>();

        services.AddScoped<ISalesPipelineStageQueries, SalesPipelineStageQueries>();
        services.AddScoped<ISalesPipelineStageCommands, SalesPipelineStageCommands>();

        services.AddScoped<IOrganizationUserQueries, OrganizationUserQueries>();

        services.AddScoped<IActivityQueries, ActivityQueries>();
        services.AddScoped<IActivityCommands, ActivityCommands>();

        services.AddScoped<IDashboardQueries, DashboardQueries>();

        return services;
    }
}