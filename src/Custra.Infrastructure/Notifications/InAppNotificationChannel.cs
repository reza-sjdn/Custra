using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Notifications;
using Custra.Domain.Notifications;
using Custra.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Notifications;

public class InAppNotificationChannel : INotificationChannel
{
    private readonly CustraDbContext _dbContext;

    public string ChannelName => "InApp";

    public InAppNotificationChannel(CustraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SendAsync(
        NotificationMessage notification,
        CancellationToken cancellationToken = default)
    {
        var entity = new Notification(
            notification.RecipientUserId,
            notification.Title,
            notification.Message,
            notification.Link);

        await _dbContext.Notifications.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}