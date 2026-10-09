using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tasks.Commands.DeleteTaskItem;

public sealed class DeleteTaskItemCommandHandler
    : IRequestHandler<DeleteTaskItemCommand, Unit>
{
    private readonly ITaskItemCommands _commands;
    private readonly IApplicationDbContext _dbContext;

    public DeleteTaskItemCommandHandler(ITaskItemCommands commands,
        IApplicationDbContext dbContext)
    {
        _commands = commands;
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(
        DeleteTaskItemCommand request,
        CancellationToken cancellationToken)
    {
        var task = await _commands.FindAsync(request.Id, cancellationToken);

        if (task is null)
        {
            throw new KeyNotFoundException("Task was not found.");
        }

        _commands.Delete(task);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}