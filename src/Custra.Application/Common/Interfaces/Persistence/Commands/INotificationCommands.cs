using Custra.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Interfaces.Persistence.Commands;

public interface INotificationCommands
{
    Task<Notification?> FindForUserAsync(
    Guid id,
    Guid userId,
    CancellationToken cancellationToken = default);
}
