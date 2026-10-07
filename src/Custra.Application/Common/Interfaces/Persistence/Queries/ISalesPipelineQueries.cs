using Custra.Application.Common.Models;
using Custra.Application.SalesPipelines.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Interfaces.Persistence.Queries;

public interface ISalesPipelineQueries
{
    Task<SalesPipelineDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<SalesPipelineDto>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<LookupItemDto>> GetLookupAsync(
        CancellationToken cancellationToken = default);

}