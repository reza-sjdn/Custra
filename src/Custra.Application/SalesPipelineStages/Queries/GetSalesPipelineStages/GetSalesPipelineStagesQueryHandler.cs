using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.SalesPipelineStages.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.SalesPipelineStages.Queries.GetSalesPipelineStages;

public sealed class GetSalesPipelineStagesQueryHandler
    : IQueryHandler<
        GetSalesPipelineStagesQuery,
        IReadOnlyList<SalesPipelineStageDto>>
{
    private readonly ISalesPipelineStageQueries _queries;

    public GetSalesPipelineStagesQueryHandler(
        ISalesPipelineStageQueries queries)
    {
        _queries = queries;
    }

    public async Task<IReadOnlyList<SalesPipelineStageDto>> Handle(
        GetSalesPipelineStagesQuery request,
        CancellationToken cancellationToken)
    {
        return await _queries.GetByPipelineIdAsync(
            request.SalesPipelineId,
            cancellationToken);
    }
}