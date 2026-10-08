using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Activities.Commands.CompleteActivity;

public sealed class CompleteActivityCommandHandler
    : ICommandHandler<CompleteActivityCommand, Unit>
{
    private readonly IActivityCommands _activityCommands;
    private readonly IApplicationDbContext _dbContext;

    public CompleteActivityCommandHandler(
        IActivityCommands activityCommands,
        IApplicationDbContext dbContext)
    {
        _activityCommands = activityCommands;
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(
        CompleteActivityCommand request,
        CancellationToken cancellationToken)
    {
        var activity = await _activityCommands.FindAsync(
            request.Id,
            cancellationToken);

        if (activity is null)
            throw new KeyNotFoundException("Activity not found.");

        activity.Complete();

        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}