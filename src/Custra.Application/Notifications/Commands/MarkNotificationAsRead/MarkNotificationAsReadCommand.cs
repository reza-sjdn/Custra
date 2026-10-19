using Custra.Application.Common.Interfaces.Authorization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Notifications.Commands.MarkNotificationAsRead;

public record MarkNotificationAsReadCommand(Guid Id)
    : IRequest<Unit>, IAuthorizationRequest
{
}