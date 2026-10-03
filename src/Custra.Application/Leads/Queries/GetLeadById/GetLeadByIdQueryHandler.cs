using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Leads.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Leads.Queries.GetLeadById;

public sealed class GetLeadByIdQueryHandler(
    ILeadQueries leadQueries)
    : IQueryHandler<GetLeadByIdQuery, LeadDto>
{
    private readonly ILeadQueries _leadQueries = leadQueries;

    public async Task<LeadDto> Handle(
        GetLeadByIdQuery request,
        CancellationToken cancellationToken)
    {
        var lead = await _leadQueries.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (lead is null)
            throw new KeyNotFoundException(
                "Lead was not found.");

        return lead;
    }
}