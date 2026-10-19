using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Notifications.Commands.SendNotification;

public class SendNotificationCommandHandler
    : IRequestHandler<SendNotificationCommand, Unit>
{
    private readonly INotificationDispatcher _dispatcher;

    public SendNotificationCommandHandler(
        INotificationDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public async Task<Unit> Handle(
        SendNotificationCommand request,
        CancellationToken cancellationToken)
    {
        var notification = new NotificationMessage(
            request.RecipientUserId,
            request.Title,
            request.Message,
            request.Link);

        await _dispatcher.DispatchAsync(notification, cancellationToken);

        return Unit.Value;
    }
}