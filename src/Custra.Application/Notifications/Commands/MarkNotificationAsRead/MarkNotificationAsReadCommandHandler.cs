using Custra.Application.Common.Interfaces;
using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Notifications.Commands.MarkNotificationAsRead;

public class MarkNotificationAsReadCommandHandler
    : IRequestHandler<MarkNotificationAsReadCommand, Unit>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly INotificationCommands _commands;
    private readonly ICurrentUserService _currentUserService;

    public MarkNotificationAsReadCommandHandler(
        IApplicationDbContext dbContext,
        INotificationCommands commands,
        ICurrentUserService currentUserService)
    {
        _dbContext = dbContext;
        _commands = commands;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(
        MarkNotificationAsReadCommand request,
        CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId.Value;

        var notification = await _commands.FindForUserAsync(
            request.Id,
            userId,
            cancellationToken);

        if (notification is null)
            throw new KeyNotFoundException("Notification not found.");

        notification.MarkAsRead();

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}