using Custra.Application.Notifications.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Interfaces.Persistence.Queries;

public interface INotificationQueries
{
    Task<IReadOnlyList<NotificationDto>> GetLatestAsync(
        int count,
        CancellationToken cancellationToken = default);

    Task<int> GetUnreadCountAsync(
        CancellationToken cancellationToken = default);
}