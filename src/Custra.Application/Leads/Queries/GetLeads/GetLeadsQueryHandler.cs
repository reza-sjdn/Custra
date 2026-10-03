using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Common.Models;
using Custra.Application.Leads.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Leads.Queries.GetLeads;

public sealed class GetLeadsQueryHandler(
    ILeadQueries leadQueries)
    : IQueryHandler<GetLeadsQuery, PagedResult<LeadDto>>
{
    private readonly ILeadQueries _leadQueries = leadQueries;

    public async Task<PagedResult<LeadDto>> Handle(
        GetLeadsQuery request,
        CancellationToken cancellationToken)
    {
        return await _leadQueries.GetPagedAsync(
            request.Search,
            request.Status,
            request.Page,
            request.PageSize,
            cancellationToken);
    }
}