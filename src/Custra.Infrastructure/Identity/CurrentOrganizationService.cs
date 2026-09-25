using Custra.Application.Common.Interfaces;
using Custra.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Custra.Infrastructure.Identity;

public sealed class CurrentOrganizationService(
    IHttpContextAccessor httpContextAccessor,
    ICurrentUserService currentUserService,
    CustraDbContext dbContext)
    : ICurrentOrganizationService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly CustraDbContext _dbContext = dbContext;

    public async Task<Guid> GetOrganizationIdAsync(
    CancellationToken cancellationToken = default)
    {
        var userId = _currentUserService.UserId;

        if (userId is null)
            throw new UnauthorizedAccessException();

        var headerValue = _httpContextAccessor.HttpContext?
            .Request.Headers["X-Organization-Id"]
            .FirstOrDefault();

        if (Guid.TryParse(headerValue, out var organizationId))
        {
            var isMember = await _dbContext.OrganizationMemberships
                .AnyAsync(
                    x => x.OrganizationId == organizationId &&
                         x.UserId == userId.Value,
                    cancellationToken);

            if (!isMember)
                throw new UnauthorizedAccessException();

            return organizationId;
        }

        var memberships = await _dbContext.OrganizationMemberships
            .Where(x => x.UserId == userId.Value)
            .Select(x => x.OrganizationId)
            .Take(2)
            .ToListAsync(cancellationToken);

        if (memberships.Count == 0)
            throw new InvalidOperationException(
                "The current user does not belong to any organization.");

        if (memberships.Count > 1)
            throw new InvalidOperationException(
                "The current user belongs to multiple organizations. An organization context is required.");

        return memberships[0];
    }
}