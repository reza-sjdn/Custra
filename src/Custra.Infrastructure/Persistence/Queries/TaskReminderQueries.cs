using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Tasks.DTOs;
using Custra.Domain.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Custra.Infrastructure.Persistence.Queries;

public class TaskReminderQueries : ITaskReminderQueries
{
    private readonly CustraDbContext _dbContext;

    public TaskReminderQueries(CustraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<TaskReminderDto>> GetDueTasksAsync(
        DateTime utcNow,
        int batchSize,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Tasks
            .Where(t =>
                t.DueDate.HasValue &&
                t.DueDate.Value <= utcNow &&
                t.DueNotificationSentAt == null &&
                t.OwnerUserId != Guid.Empty &&
                (t.Status != TaskItemStatus.Completed && t.Status != TaskItemStatus.Cancelled))
            .OrderBy(t => t.DueDate)
            .Take(batchSize)
            .Select(t => new TaskReminderDto(
                t.Id,
                t.OwnerUserId,
                t.Title,
                t.DueDate!.Value))
            .ToListAsync(cancellationToken);
    }
}
