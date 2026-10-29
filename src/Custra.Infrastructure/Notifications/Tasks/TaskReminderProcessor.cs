using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Notifications;
using Custra.Application.Tasks.Reminders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Notifications.Tasks;

public class TaskReminderProcessor : ITaskReminderProcessor
{
    private readonly ITaskReminderQueries _queries;
    private readonly INotificationDispatcher _dispatcher;
    private readonly ITaskItemCommands _taskCommands;

    public TaskReminderProcessor(
        ITaskReminderQueries queries,
        INotificationDispatcher dispatcher,
        ITaskItemCommands taskCommands)
    {
        _queries = queries;
        _dispatcher = dispatcher;
        _taskCommands = taskCommands;
    }

    public async Task ProcessDueTasksAsync(
        CancellationToken cancellationToken = default)
    {
        var tasks = await _queries.GetDueTasksAsync(
            DateTime.UtcNow, 100, cancellationToken);

        foreach (var task in tasks)
        {
            try
            {
                await _dispatcher.DispatchAsync(
                    new NotificationMessage(
                        task.AssigneeUserId,
                        "Task due",
                        $"The task '{task.Title}' is due.",
                        $"/Tasks/Details/{task.TaskId}"),
                    cancellationToken);

                await _taskCommands.MarkDueNotificationSentAsync(
                    task.TaskId, cancellationToken);
            }
            catch (OperationCanceledException)
                when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception)
            {
                // Log the failure here; continue processing other tasks.
            }
        }
    }
}