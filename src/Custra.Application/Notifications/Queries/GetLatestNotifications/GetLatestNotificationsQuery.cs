using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Notifications.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Notifications.Queries.GetLatestNotifications;

public record GetLatestNotificationsQuery(int Count = 100)
    : IRequest<IReadOnlyList<NotificationDto>>, IAuthorizationRequest
{
}