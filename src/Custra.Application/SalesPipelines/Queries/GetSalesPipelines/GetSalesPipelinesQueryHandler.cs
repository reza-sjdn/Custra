using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.SalesPipelines.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.SalesPipelines.Queries.GetSalesPipelines;

public sealed class GetSalesPipelinesQueryHandler
    : IQueryHandler<GetSalesPipelinesQuery, IReadOnlyList<SalesPipelineDto>>
{
    private readonly ISalesPipelineQueries _queries;

    public GetSalesPipelinesQueryHandler(
        ISalesPipelineQueries queries)
    {
        _queries = queries;
    }

    public async Task<IReadOnlyList<SalesPipelineDto>> Handle(
        GetSalesPipelinesQuery request,
        CancellationToken cancellationToken)
    {
        return await _queries.GetAllAsync(cancellationToken);
    }
}