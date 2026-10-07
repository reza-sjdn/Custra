using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.SalesPipelineStages.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.SalesPipelineStages.Queries.GetSalesPipelineStageById;

public sealed class GetSalesPipelineStageByIdQueryHandler
    : IQueryHandler<
        GetSalesPipelineStageByIdQuery,
        SalesPipelineStageDto>
{
    private readonly ISalesPipelineStageQueries _queries;

    public GetSalesPipelineStageByIdQueryHandler(
        ISalesPipelineStageQueries queries)
    {
        _queries = queries;
    }

    public async Task<SalesPipelineStageDto> Handle(
        GetSalesPipelineStageByIdQuery request,
        CancellationToken cancellationToken)
    {
        var stage = await _queries.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (stage is null)
            throw new KeyNotFoundException(
                "Sales pipeline stage was not found.");

        return stage;
    }
}