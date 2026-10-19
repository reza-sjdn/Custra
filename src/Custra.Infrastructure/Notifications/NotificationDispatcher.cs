using Custra.Application.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Notifications;

public class NotificationDispatcher : INotificationDispatcher
{
    private readonly IEnumerable<INotificationChannel> _channels;

    public NotificationDispatcher(
        IEnumerable<INotificationChannel> channels)
    {
        _channels = channels;
    }

    public async Task DispatchAsync(
        NotificationMessage notification,
        CancellationToken cancellationToken = default)
    {
        foreach (var channel in _channels)
        {
            await channel.SendAsync(notification, cancellationToken);
        }
    }
}