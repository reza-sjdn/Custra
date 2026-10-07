using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Opportunities.Commands.MarkOpportunityAsWon;

public sealed record MarkOpportunityAsWonCommand(
    Guid Id)
    : ICommand<Unit>,
      IAuthorizationRequest;