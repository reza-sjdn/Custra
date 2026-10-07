using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Opportunities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Opportunities.Queries.GetOpportunityById;

public sealed class GetOpportunityByIdQueryHandler
    : IQueryHandler<GetOpportunityByIdQuery, OpportunityDto>
{
    private readonly IOpportunityQueries _queries;

    public GetOpportunityByIdQueryHandler(
        IOpportunityQueries queries)
    {
        _queries = queries;
    }

    public async Task<OpportunityDto> Handle(
        GetOpportunityByIdQuery request,
        CancellationToken cancellationToken)
    {
        var opportunity = await _queries.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (opportunity is null)
            throw new KeyNotFoundException(
                "Opportunity was not found.");

        return opportunity;
    }
}