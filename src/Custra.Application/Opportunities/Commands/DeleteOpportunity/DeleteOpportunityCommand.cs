using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Opportunities.Commands.DeleteOpportunity;

public sealed record DeleteOpportunityCommand(
    Guid Id)
    : ICommand<Unit>,
      IAuthorizationRequest;