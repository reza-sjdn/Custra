using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Opportunities.Commands.CreateOpportunity;

public sealed record CreateOpportunityCommand(
    Guid CustomerId,
    Guid? ContactId,
    Guid OwnerUserId,
    Guid SalesPipelineId,
    Guid SalesPipelineStageId,
    string Title,
    string? Description,
    decimal? EstimatedValue,
    DateTime? ExpectedCloseDate)
    : ICommand<Guid>,
      IAuthorizationRequest;