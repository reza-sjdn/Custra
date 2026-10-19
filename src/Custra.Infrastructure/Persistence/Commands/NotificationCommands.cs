using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Domain.Notifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Commands;

public class NotificationCommands(CustraDbContext _dbContext) : INotificationCommands
{
    public Task<Notification?> FindForUserAsync(
    Guid id,
    Guid userId,
    CancellationToken cancellationToken = default)
    {
        return _dbContext.Notifications.FirstOrDefaultAsync(
            x => x.Id == id && x.RecipientUserId == userId,
            cancellationToken);
    }
}
