using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Application.Tasks.Commands.CancelTaskItem;
using Custra.Application.Tasks.Commands.CompleteTaskItem;
using Custra.Application.Tasks.Commands.ReopenTaskItem;
using Custra.Application.Tasks.Commands.StartTaskItem;
using Custra.Domain.Tasks;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tasks.Commands;

public sealed class TaskItemStatusCommandHandler :
    IRequestHandler<StartTaskItemCommand, Unit>,
    IRequestHandler<CompleteTaskItemCommand, Unit>,
    IRequestHandler<CancelTaskItemCommand, Unit>,
    IRequestHandler<ReopenTaskItemCommand, Unit>
{
    private readonly ITaskItemCommands _commands;
    private readonly IApplicationDbContext _dbContext;

    public TaskItemStatusCommandHandler(
        ITaskItemCommands commands,
        IApplicationDbContext dbContext)
    {
        _commands = commands;
        _dbContext = dbContext;
    }

    public Task<Unit> Handle(
        StartTaskItemCommand request,
        CancellationToken cancellationToken)
    {
        return ChangeStatusAsync(request.Id, x => x.Start(), cancellationToken);
    }

    public Task<Unit> Handle(
        CompleteTaskItemCommand request,
        CancellationToken cancellationToken)
    {
        return ChangeStatusAsync(request.Id, x => x.Complete(), cancellationToken);
    }

    public Task<Unit> Handle(
        CancelTaskItemCommand request,
        CancellationToken cancellationToken)
    {
        return ChangeStatusAsync(request.Id, x => x.Cancel(), cancellationToken);
    }

    public Task<Unit> Handle(
        ReopenTaskItemCommand request,
        CancellationToken cancellationToken)
    {
        return ChangeStatusAsync(request.Id, x => x.Reopen(), cancellationToken);
    }

    private async Task<Unit> ChangeStatusAsync(
        Guid id,
        Action<TaskItem> changeStatus,
        CancellationToken cancellationToken)
    {
        var task = await _commands.FindAsync(id, cancellationToken);

        if (task is null)
        {
            throw new KeyNotFoundException("Task was not found.");
        }

        changeStatus(task);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}