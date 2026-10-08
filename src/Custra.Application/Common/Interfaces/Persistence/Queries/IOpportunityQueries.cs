using Custra.Application.Common.Models;
using Custra.Application.Opportunities.DTOs;
using Custra.Domain.Opportunities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Interfaces.Persistence.Queries;

public interface IOpportunityQueries
{
    Task<OpportunityDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<PagedResult<OpportunityDto>> GetPagedAsync(
        string? search,
        OpportunityStatus? status,
        Guid? customerId,
        Guid? ownerUserId,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsForPipelineAsync(
        Guid salesPipelineId,
        CancellationToken cancellationToken = default);

    Task<bool> StageBelongsToPipelineAsync(
        Guid stageId,
        Guid pipelineId,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<LookupItemDto>> GetLookupAsync(
        CancellationToken cancellationToken = default);

}