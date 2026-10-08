using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Activities.Commands.DeleteActivity;

public sealed class DeleteActivityCommandHandler
    : ICommandHandler<DeleteActivityCommand, Unit>
{
    private readonly IActivityCommands _activityCommands;
    private readonly IApplicationDbContext _dbContext;

    public DeleteActivityCommandHandler(
        IActivityCommands activityCommands,
        IApplicationDbContext dbContext)
    {
        _activityCommands = activityCommands;
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(
        DeleteActivityCommand request,
        CancellationToken cancellationToken)
    {
        var activity = await _activityCommands.FindAsync(
            request.Id,
            cancellationToken);

        if (activity is null)
            throw new KeyNotFoundException("Activity not found.");

        _activityCommands.Delete(activity);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}