using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tasks.Reminders;

public interface ITaskReminderProcessor
{
    Task ProcessDueTasksAsync(
        CancellationToken cancellationToken = default);
}