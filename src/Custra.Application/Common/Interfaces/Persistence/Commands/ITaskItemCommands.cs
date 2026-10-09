using Custra.Domain.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Interfaces.Persistence.Commands;

public interface ITaskItemCommands
{
    Task AddAsync(
        TaskItem task,
        CancellationToken cancellationToken = default);

    Task<TaskItem?> FindAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    void Delete(TaskItem task);
}