using Custra.Application.Common.Authorization;
using Custra.Domain.Authorization;
using Custra.Domain.Opportunities;
using Custra.Domain.Organizations;
using Custra.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence;

public static class DevelopmentDataSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var userManager =
            services.GetRequiredService<UserManager<ApplicationUser>>();

        var dbContext =
            services.GetRequiredService<CustraDbContext>();

        const string email = "admin@custra.local";
        const string password = "Admin123!";

        // Create/find admin user
        var user = await userManager.FindByEmailAsync(email);

        if (user is null)
        {
            user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(
                    string.Join(
                        "; ",
                        result.Errors.Select(x => x.Description)));
            }
        }

        // Create/find organization
        var organization = await dbContext.Organizations
            .FirstOrDefaultAsync();

        if (organization is null)
        {
            organization = new Organization("Custra Development");

            dbContext.Organizations.Add(organization);
            await dbContext.SaveChangesAsync();
        }

        var organizationContext =
            services.GetRequiredService<OrganizationContext>();

        organizationContext.SetOrganization(organization.Id);

        // Seed the default Sales Pipeline and its Stages
        await SeedDefaultSalesPipelineAsync(dbContext, organization);

        // Seed permissions
        await SeedPermissionsAsync(dbContext);

        // Create/find Admin role
        var adminRole = await SeedAdminRoleAsync(
            dbContext,
            organization.Id);

        // Assign all permissions to Admin
        await SeedAdminRolePermissionsAsync(
            dbContext,
            adminRole.Id);

        // Create/find SalesPerson role
        var salespersonRole = await SeedSalespersonRoleAsync(
            dbContext,
            organization.Id);

        // Assign permissions to SalesPerson
        await SeedSalespersonRolePermissionsAsync(
            dbContext,
            salespersonRole.Id);

        // Create membership
        var membership = await dbContext.OrganizationMemberships
            .SingleOrDefaultAsync(x =>
                x.OrganizationId == organization.Id &&
                x.UserId == user.Id);

        if (membership is null)
        {
            dbContext.OrganizationMemberships.Add(
                new OrganizationMembership(
                    organization.Id,
                    user.Id,
                    adminRole.Id));

            await dbContext.SaveChangesAsync();
        }

        const string salespersonEmail = "salesperson@custra.local";
        const string salespersonPassword = "Salesperson123!";

        var salespersonUser =
            await userManager.FindByEmailAsync(salespersonEmail);

        if (salespersonUser is null)
        {
            salespersonUser = new ApplicationUser
            {
                UserName = salespersonEmail,
                Email = salespersonEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(
                salespersonUser,
                salespersonPassword);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(
                    string.Join(
                        "; ",
                        result.Errors.Select(x => x.Description)));
            }
        }

        var salespersonMembershipExists =
            await dbContext.OrganizationMemberships.AnyAsync(x =>
                x.OrganizationId == organization.Id &&
                x.UserId == salespersonUser.Id);

        if (!salespersonMembershipExists)
        {
            dbContext.OrganizationMemberships.Add(
                new OrganizationMembership(
                    organization.Id,
                    salespersonUser.Id,
                    salespersonRole.Id));

            await dbContext.SaveChangesAsync();
        }
    }

    private static async Task SeedPermissionsAsync(
        CustraDbContext dbContext)
    {
        var permissions = new[]
        {
            #region Customer
            new Permission(
                Permissions.Customers.View,
                "View customers"),

            new Permission(
                Permissions.Customers.Create,
                "Create customers"),

            new Permission(
                Permissions.Customers.Update,
                "Update customers"),

            new Permission(
                Permissions.Customers.Delete,
                "Delete customers"),
            #endregion

            #region Contact
            new Permission(
                Permissions.Contacts.View,
                "View contacts"),

            new Permission(
                Permissions.Contacts.Create,
                "Create contacts"),

            new Permission(
                Permissions.Contacts.Update,
                "Update contacts"),

            new Permission(
                Permissions.Contacts.Delete,
                "Delete contacts"),
            #endregion

            #region Leads
            new Permission(
                Permissions.Leads.View,
                "View leads"),

            new Permission(
                Permissions.Leads.Create,
                "Create leads"),

            new Permission(
                Permissions.Leads.Update,
                "Update leads"),

            new Permission(
                Permissions.Leads.Delete,
                "Delete leads"),
	        #endregion

            #region SalesPipelines
            new Permission(
                Permissions.Pipelines.View,
                "View sales pipelines"),

            new Permission(
                Permissions.Pipelines.Create,
                "Create sales pipelines"),

            new Permission(
                Permissions.Pipelines.Update,
                "Update sales pipelines"),

            new Permission(
                Permissions.Pipelines.Delete,
                "Delete sales pipelines"),
	        #endregion

            #region Opportunities
            new Permission(
                Permissions.Opportunities.View,
                "View opportunities"),

            new Permission(
                Permissions.Opportunities.Create,
                "Create opportunities"),

            new Permission(
                Permissions.Opportunities.Update,
                "Update opportunities"),

            new Permission(
                Permissions.Opportunities.Delete,
                "Delete opportunities")
	        #endregion

        };

        foreach (var permission in permissions)
        {
            var exists = await dbContext.Permissions
                .AnyAsync(x => x.Name == permission.Name);

            if (!exists)
            {
                dbContext.Permissions.Add(permission);
            }
        }

        await dbContext.SaveChangesAsync();
    }

    private static async Task<Role> SeedAdminRoleAsync(
        CustraDbContext dbContext,
        Guid organizationId)
    {
        var role = await dbContext.Roles
            .SingleOrDefaultAsync(x =>
                x.OrganizationId == organizationId &&
                x.Name == "Admin");

        if (role is null)
        {
            role = new Role(
                organizationId,
                "Admin");

            dbContext.Roles.Add(role);
            await dbContext.SaveChangesAsync();
        }

        return role;
    }

    private static async Task<Role> SeedSalespersonRoleAsync(
        CustraDbContext dbContext,
        Guid organizationId)
    {
        var role = await dbContext.Roles
            .SingleOrDefaultAsync(x =>
                x.OrganizationId == organizationId &&
                x.Name == "Salesperson");

        if (role is null)
        {
            role = new Role(
                organizationId,
                "Salesperson");

            dbContext.Roles.Add(role);
            await dbContext.SaveChangesAsync();
        }

        return role;
    }

    private static async Task SeedAdminRolePermissionsAsync(
        CustraDbContext dbContext,
        Guid roleId)
    {
        var permissionIds = await dbContext.Permissions
            .Select(x => x.Id)
            .ToListAsync();

        foreach (var permissionId in permissionIds)
        {
            var exists = await dbContext.RolePermissions
                .AnyAsync(x =>
                    x.RoleId == roleId &&
                    x.PermissionId == permissionId);

            if (!exists)
            {
                dbContext.RolePermissions.Add(
                    new RolePermission(
                        roleId,
                        permissionId));
            }
        }

        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedSalespersonRolePermissionsAsync(
        CustraDbContext dbContext,
        Guid roleId)
    {
        var permissionIds = await dbContext.Permissions
            .Where(x => x.Name == Permissions.Customers.View ||
                        x.Name == Permissions.Contacts.View ||
                        x.Name == Permissions.Leads.View)
            .Select(x => x.Id)
            .ToListAsync();

        foreach (var permissionId in permissionIds)
        {
            var exists = await dbContext.RolePermissions
                .AnyAsync(x =>
                    x.RoleId == roleId &&
                    x.PermissionId == permissionId);

            if (!exists)
            {
                dbContext.RolePermissions.Add(
                    new RolePermission(
                        roleId,
                        permissionId));
            }
        }

        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedDefaultSalesPipelineAsync(
        CustraDbContext dbContext,
        Organization organization)
    {
        var pipeline = await dbContext.SalesPipelines
            .FirstOrDefaultAsync(x =>
                x.OrganizationId == organization.Id &&
                x.Name == "Default Sales Pipeline");

        if (pipeline is null)
        {
            pipeline = new SalesPipeline("Default Sales Pipeline");
            await dbContext.SalesPipelines.AddAsync(pipeline);

            var stages = new[]
            {
                new SalesPipelineStage(
                    pipeline.Id,
                    "New",
                    1,
                    10),

                new SalesPipelineStage(
                    pipeline.Id,
                    "Qualified",
                    2,
                    30),

                new SalesPipelineStage(
                    pipeline.Id,
                    "Proposal",
                    3,
                    60),

                new SalesPipelineStage(
                    pipeline.Id,
                    "Negotiation",
                    4,
                    80)
            };

            await dbContext.SalesPipelineStages.AddRangeAsync(stages);
            await dbContext.SaveChangesAsync();
        }
    }

}