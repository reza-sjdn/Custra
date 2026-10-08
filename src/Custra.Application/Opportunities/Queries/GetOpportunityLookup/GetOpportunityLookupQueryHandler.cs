using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Opportunities.Queries.GetOpportunityLookup;

public sealed class GetOpportunityLookupQueryHandler
    : IQueryHandler<GetOpportunityLookupQuery, IReadOnlyList<LookupItemDto>>
{
    private readonly IOpportunityQueries _opportunityQueries;

    public GetOpportunityLookupQueryHandler(
        IOpportunityQueries opportunityQueries)
    {
        _opportunityQueries = opportunityQueries;
    }

    public async Task<IReadOnlyList<LookupItemDto>> Handle(
        GetOpportunityLookupQuery request,
        CancellationToken cancellationToken)
    {
        return await _opportunityQueries.GetLookupAsync(cancellationToken);
    }
}