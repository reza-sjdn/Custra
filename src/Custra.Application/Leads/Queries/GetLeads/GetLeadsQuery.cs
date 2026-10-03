using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Models;
using Custra.Application.Leads.DTOs;
using Custra.Domain.Leads;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Leads.Queries.GetLeads;

public sealed record GetLeadsQuery(
    string? Search,
    LeadStatus? Status,
    int Page = 1,
    int PageSize = 10)
    : IQuery<PagedResult<LeadDto>>, IAuthorizationRequest;