using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Opportunities.Commands.UpdateOpportunity;

public sealed record UpdateOpportunityCommand(
    Guid Id,
    Guid CustomerId,
    Guid? ContactId,
    Guid OwnerUserId,
    Guid SalesPipelineId,
    Guid SalesPipelineStageId,
    string Title,
    string? Description,
    decimal? EstimatedValue,
    DateTime? ExpectedCloseDate)
    : ICommand<Unit>,
      IAuthorizationRequest;