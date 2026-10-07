using Custra.Application.Common.Models;
using Custra.Application.SalesPipelineStages.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Interfaces.Persistence.Queries;

public interface ISalesPipelineStageQueries
{
    Task<SalesPipelineStageDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<SalesPipelineStageDto>> GetByPipelineIdAsync(
        Guid salesPipelineId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<LookupItemDto>> GetLookupByPipelineIdAsync(
        Guid pipelineId,
        CancellationToken cancellationToken = default);

}