using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Common.Models;
using Custra.Application.Opportunities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Opportunities.Queries.GetOpportunities;

public sealed class GetOpportunitiesQueryHandler
    : IQueryHandler<
        GetOpportunitiesQuery,
        PagedResult<OpportunityDto>>
{
    private readonly IOpportunityQueries _queries;

    public GetOpportunitiesQueryHandler(
        IOpportunityQueries queries)
    {
        _queries = queries;
    }

    public async Task<PagedResult<OpportunityDto>> Handle(
        GetOpportunitiesQuery request,
        CancellationToken cancellationToken)
    {
        return await _queries.GetPagedAsync(
            request.Search,
            request.Status,
            request.CustomerId,
            request.OwnerUserId,
            request.Page,
            request.PageSize,
            cancellationToken);
    }
}