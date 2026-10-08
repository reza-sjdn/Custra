using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using Custra.Domain.Activities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Activities.Commands.UpdateActivity;

public sealed record UpdateActivityCommand(
    Guid Id,
    string Subject,
    string? Description,
    ActivityType Type,
    Guid OwnerUserId,
    DateTime? DueDate,
    Guid? CustomerId,
    Guid? ContactId,
    Guid? LeadId,
    Guid? OpportunityId)
    : ICommand<Unit>,
      IAuthorizationRequest;