using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.SalesPipelines.Queries.GetSalesPipelineLookup;

public sealed class GetSalesPipelineLookupQueryHandler
    : IQueryHandler<
        GetSalesPipelineLookupQuery,
        IReadOnlyList<LookupItemDto>>
{
    private readonly ISalesPipelineQueries _pipelineQueries;

    public GetSalesPipelineLookupQueryHandler(
        ISalesPipelineQueries pipelineQueries)
    {
        _pipelineQueries = pipelineQueries;
    }

    public Task<IReadOnlyList<LookupItemDto>> Handle(
        GetSalesPipelineLookupQuery request,
        CancellationToken cancellationToken)
    {
        return _pipelineQueries.GetLookupAsync(cancellationToken);
    }
}