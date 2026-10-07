using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Opportunities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Opportunities.Queries.GetOpportunityById;

public sealed record GetOpportunityByIdQuery(
    Guid Id)
    : IQuery<OpportunityDto>,
      IAuthorizationRequest;