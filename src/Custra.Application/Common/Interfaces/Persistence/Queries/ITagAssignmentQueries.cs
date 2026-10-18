using Custra.Application.Tags.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Interfaces.Persistence.Queries;


public interface ITagAssignmentQueries
{
    Task<IReadOnlyList<TagAssignmentDto>> GetAssignedTagsAsync(
        string recordType,
        Guid recordId,
        CancellationToken cancellationToken = default);

    Task<bool> HasAssignmentsAsync(
        Guid tagId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TagUsageDto>> GetUsagesAsync(
        Guid tagId,
        CancellationToken cancellationToken = default);

}
