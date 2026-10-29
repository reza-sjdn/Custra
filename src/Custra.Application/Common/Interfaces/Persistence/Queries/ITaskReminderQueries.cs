using Custra.Application.Tasks.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Interfaces.Persistence.Queries;

public interface ITaskReminderQueries
{
    Task<IReadOnlyList<TaskReminderDto>> GetDueTasksAsync(
        DateTime utcNow,
        int batchSize,
        CancellationToken cancellationToken = default);
}