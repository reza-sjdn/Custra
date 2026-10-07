using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.SalesPipelines.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.SalesPipelines.Queries.GetSalesPipelineById;

public sealed class GetSalesPipelineByIdQueryHandler
    : IQueryHandler<GetSalesPipelineByIdQuery, SalesPipelineDto>
{
    private readonly ISalesPipelineQueries _queries;

    public GetSalesPipelineByIdQueryHandler(
        ISalesPipelineQueries queries)
    {
        _queries = queries;
    }

    public async Task<SalesPipelineDto> Handle(
        GetSalesPipelineByIdQuery request,
        CancellationToken cancellationToken)
    {
        var pipeline = await _queries.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (pipeline is null)
            throw new KeyNotFoundException("Sales pipeline was not found.");

        return pipeline;
    }
}