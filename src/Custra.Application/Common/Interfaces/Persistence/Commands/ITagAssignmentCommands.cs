using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Interfaces.Persistence.Commands;


public interface ITagAssignmentCommands
{
    Task<bool> ExistsAsync(
        string recordType,
        Guid recordId,
        Guid tagId,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        string recordType,
        Guid recordId,
        Guid tagId,
        CancellationToken cancellationToken = default);

    Task RemoveAsync(
        string recordType,
        Guid recordId,
        Guid tagId,
        CancellationToken cancellationToken = default);
}
