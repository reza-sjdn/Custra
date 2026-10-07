using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.SalesPipelineStages.Queries.GetSalesPipelineStageLookup;

public sealed class GetSalesPipelineStageLookupQueryHandler
    : IQueryHandler<
        GetSalesPipelineStageLookupQuery,
        IReadOnlyList<LookupItemDto>>
{
    private readonly ISalesPipelineStageQueries _stageQueries;

    public GetSalesPipelineStageLookupQueryHandler(
        ISalesPipelineStageQueries stageQueries)
    {
        _stageQueries = stageQueries;
    }

    public Task<IReadOnlyList<LookupItemDto>> Handle(
        GetSalesPipelineStageLookupQuery request,
        CancellationToken cancellationToken)
    {
        return _stageQueries.GetLookupByPipelineIdAsync(
            request.SalesPipelineId,
            cancellationToken);
    }
}