using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Domain.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Commands;

public sealed class TaskItemCommands : ITaskItemCommands
{
    private readonly CustraDbContext _dbContext;

    public TaskItemCommands(CustraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(
        TaskItem task,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Tasks.AddAsync(task, cancellationToken);
    }

    public async Task<TaskItem?> FindAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Tasks
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public void Delete(TaskItem task)
    {
        _dbContext.Tasks.Remove(task);
    }
}