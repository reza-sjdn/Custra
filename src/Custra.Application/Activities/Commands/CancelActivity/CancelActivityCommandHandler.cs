using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Activities.Commands.CancelActivity;

public sealed class CancelActivityCommandHandler
    : ICommandHandler<CancelActivityCommand, Unit>
{
    private readonly IActivityCommands _activityCommands;
    private readonly IApplicationDbContext _dbContext;

    public CancelActivityCommandHandler(
        IActivityCommands activityCommands,
        IApplicationDbContext dbContext)
    {
        _activityCommands = activityCommands;
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(
        CancelActivityCommand request,
        CancellationToken cancellationToken)
    {
        var activity = await _activityCommands.FindAsync(
            request.Id,
            cancellationToken);

        if (activity is null)
            throw new KeyNotFoundException("Activity not found.");

        activity.Cancel();

        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}