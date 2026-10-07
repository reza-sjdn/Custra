using Custra.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Interfaces.Persistence.Queries;

public interface IOrganizationUserQueries
{
    Task<IReadOnlyList<LookupItemDto>> GetLookupAsync(
        CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(
        Guid userId,
        CancellationToken cancellationToken = default);

}