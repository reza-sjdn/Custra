using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Notifications;

public interface INotificationChannel
{
    string ChannelName { get; }

    Task SendAsync(
        NotificationMessage notification,
        CancellationToken cancellationToken = default);
}