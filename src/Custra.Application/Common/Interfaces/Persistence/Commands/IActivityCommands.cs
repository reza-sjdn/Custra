using Custra.Domain.Activities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Interfaces.Persistence.Commands;

public interface IActivityCommands
{
    Task AddAsync(
        Activity activity,
        CancellationToken cancellationToken = default);

    Task<Activity?> FindAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    void Delete(Activity activity);
}