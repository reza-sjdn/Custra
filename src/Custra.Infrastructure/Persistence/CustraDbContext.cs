using Custra.Application.Common.Interfaces;
using Custra.Application.Common.Interfaces.Persistence;
using Custra.Domain.Authorization;
using Custra.Domain.Common;
using Custra.Domain.Contacts;
using Custra.Domain.Customers;
using Custra.Domain.Organizations;
using Custra.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Custra.Infrastructure.Persistence;

public class CustraDbContext
    : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>,
      IApplicationDbContext
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IOrganizationContext _organizationContext;

    public CustraDbContext(
        DbContextOptions<CustraDbContext> options,
        ICurrentUserService currentUserService,
        IOrganizationContext organizationContext)
        : base(options)
    {
        _currentUserService = currentUserService;
        _organizationContext = organizationContext;
    }

    public Guid CurrentOrganizationId =>
        _organizationContext.OrganizationId;

    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<OrganizationMembership> OrganizationMemberships => Set<OrganizationMembership>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public new DbSet<Role> Roles => Set<Role>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Contact> Contacts => Set<Contact>();

    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await ApplyAuditingAsync(cancellationToken);

        return await base.SaveChangesAsync(cancellationToken);
    }

    private async Task ApplyAuditingAsync(CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var userId = _currentUserService.UserId;

        var organizationOwnedEntries = ChangeTracker
            .Entries<OrganizationOwnedEntity>()
            .Where(x => x.State == EntityState.Added)
            .ToList();

        if (organizationOwnedEntries.Count > 0)
        {
            var organizationId = _organizationContext.OrganizationId;

            if (organizationId == Guid.Empty)
                throw new InvalidOperationException(
                    "An organization context is required to create organization-owned entities.");

            foreach (var entry in organizationOwnedEntries)
            {
                entry.Property(nameof(OrganizationOwnedEntity.OrganizationId))
                    .CurrentValue = organizationId;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(nameof(AuditableEntity.CreatedAt)).CurrentValue = now;
                entry.Property(nameof(AuditableEntity.CreatedBy)).CurrentValue = userId;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Property(nameof(AuditableEntity.ModifiedAt)).CurrentValue = now;
                entry.Property(nameof(AuditableEntity.ModifiedBy)).CurrentValue = userId;
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(CustraDbContext).Assembly);

        foreach (var entityType in modelBuilder.Model
            .GetEntityTypes()
            .Where(x => typeof(OrganizationOwnedEntity)
            .IsAssignableFrom(x.ClrType)))
        {
            var parameter = Expression.Parameter(entityType.ClrType, "e");

            var organizationIdProperty = Expression.Property(
                parameter,
                nameof(OrganizationOwnedEntity.OrganizationId));

            var currentOrganizationId = Expression.Property(
                Expression.Constant(this),
                nameof(CurrentOrganizationId));

            var body = Expression.Equal(
                organizationIdProperty,
                currentOrganizationId);

            var lambda = Expression.Lambda(body, parameter);

            modelBuilder.Entity(entityType.ClrType)
                .HasQueryFilter(lambda);
        }
    }

}