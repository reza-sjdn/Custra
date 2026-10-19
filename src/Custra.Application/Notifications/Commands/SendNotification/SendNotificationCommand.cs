using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Custra.Application.Notifications.Commands.SendNotification;

public record SendNotificationCommand(
    Guid RecipientUserId,
    string Title,
    string Message,
    string? Link = null) : IRequest<Unit>;