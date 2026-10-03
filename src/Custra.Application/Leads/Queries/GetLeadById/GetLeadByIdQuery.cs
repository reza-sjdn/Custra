using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Leads.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Leads.Queries.GetLeadById;

public sealed record GetLeadByIdQuery(Guid Id)
    : IQuery<LeadDto>, IAuthorizationRequest;