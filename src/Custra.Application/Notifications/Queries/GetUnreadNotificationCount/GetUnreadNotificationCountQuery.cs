using Custra.Application.Common.Interfaces.Authorization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Notifications.Queries.GetUnreadNotificationCount;

public record GetUnreadNotificationCountQuery()
    : IRequest<int>, IAuthorizationRequest
{
}