using Custra.Application.Common.Interfaces;
using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Notifications.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Queries;

public class NotificationQueries : INotificationQueries
{
    private readonly CustraDbContext _dbContext;
    private readonly ICurrentUserService _currentUser;
    private readonly ICurrentOrganizationService _organization;

    public NotificationQueries(
        CustraDbContext dbContext,
        ICurrentUserService currentUser,
        ICurrentOrganizationService organization)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
        _organization = organization;
    }

    public async Task<IReadOnlyList<NotificationDto>> GetLatestAsync(
        int count,
        CancellationToken cancellationToken = default)
    {
        var userId = _currentUser.UserId;
        var organizationId = await _organization.GetOrganizationIdAsync();

        return await _dbContext.Notifications
            .AsNoTracking()
            .Where(x => x.RecipientUserId == userId &&
                        x.OrganizationId == organizationId)
            .OrderByDescending(x => x.CreatedAt)
            .Take(count)
            .Select(x => new NotificationDto(
                x.Id, x.Title, x.Message, x.Link,
                x.CreatedAt, x.ReadAt))
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetUnreadCountAsync(
        CancellationToken cancellationToken = default)
    {
        var userId = _currentUser.UserId;
        var organizationId = await _organization.GetOrganizationIdAsync();

        return await _dbContext.Notifications
            .CountAsync(x => x.RecipientUserId == userId &&
                             x.OrganizationId == organizationId &&
                             x.ReadAt == null,
                        cancellationToken);
    }
}