using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Queries;

public class OrganizationUserQueries : IOrganizationUserQueries
{
    private readonly CustraDbContext _dbContext;

    public OrganizationUserQueries(CustraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<LookupItemDto>> GetLookupAsync(
        CancellationToken cancellationToken = default)
    {
        return await (
            from membership in _dbContext.OrganizationMemberships
            join user in _dbContext.Users
                on membership.UserId equals user.Id
            orderby user.UserName
            select new LookupItemDto(
                user.Id,
                user.UserName!)
        )
        .AsNoTracking()
        .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.OrganizationMemberships
            .AsNoTracking()
            .AnyAsync(
                x => x.UserId == userId,
                cancellationToken);
    }

}
