using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Notifications;

public interface INotificationDispatcher
{
    Task DispatchAsync(
        NotificationMessage notification,
        CancellationToken cancellationToken = default);
}