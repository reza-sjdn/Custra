using Custra.Application.Common.Interfaces.Authorization;
using Custra.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Authorization;

public sealed class AuthorizationService(
    CustraDbContext dbContext)
    : IAuthorizationService
{
    private readonly CustraDbContext _dbContext = dbContext;

    public async Task<bool> HasPermissionAsync(
        Guid userId,
        Guid organizationId,
        string permission,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.OrganizationMemberships
            .AsNoTracking()
            .Where(x =>
                x.UserId == userId &&
                x.OrganizationId == organizationId)
            .SelectMany(x => x.Role.RolePermissions)
            .AnyAsync(
                x => x.Permission.Name == permission,
                cancellationToken);
    }
}
