using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Notifications.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Notifications.Queries.GetLatestNotifications;

public class GetLatestNotificationsQueryHandler
    : IRequestHandler<GetLatestNotificationsQuery,
        IReadOnlyList<NotificationDto>>
{
    private readonly INotificationQueries _queries;

    public GetLatestNotificationsQueryHandler(INotificationQueries queries)
    {
        _queries = queries;
    }

    public Task<IReadOnlyList<NotificationDto>> Handle(
        GetLatestNotificationsQuery request,
        CancellationToken cancellationToken)
    {
        return _queries.GetLatestAsync(
            Math.Clamp(request.Count, 1, 100),
            cancellationToken);
    }
}