using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Models;
using Custra.Application.Opportunities.DTOs;
using Custra.Domain.Opportunities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Opportunities.Queries.GetOpportunities;

public sealed record GetOpportunitiesQuery(
    string? Search = null,
    OpportunityStatus? Status = null,
    Guid? CustomerId = null,
    Guid? OwnerUserId = null,
    int Page = 1,
    int PageSize = 10)
    : IQuery<PagedResult<OpportunityDto>>,
      IAuthorizationRequest;