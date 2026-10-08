using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Leads.Queries.GetLeadLookup;

public sealed class GetLeadLookupQueryHandler
    : IQueryHandler<GetLeadLookupQuery, IReadOnlyList<LookupItemDto>>
{
    private readonly ILeadQueries _leadQueries;

    public GetLeadLookupQueryHandler(ILeadQueries leadQueries)
    {
        _leadQueries = leadQueries;
    }

    public async Task<IReadOnlyList<LookupItemDto>> Handle(
        GetLeadLookupQuery request,
        CancellationToken cancellationToken)
    {
        return await _leadQueries.GetLookupAsync(cancellationToken);
    }
}