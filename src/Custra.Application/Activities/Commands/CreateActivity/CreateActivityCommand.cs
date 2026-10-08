using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using Custra.Domain.Activities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Activities.Commands.CreateActivity;

public sealed record CreateActivityCommand(
    string Subject,
    string? Description,
    ActivityType Type,
    Guid OwnerUserId,
    DateTime? DueDate,
    Guid? CustomerId,
    Guid? ContactId,
    Guid? LeadId,
    Guid? OpportunityId)
    : ICommand<Guid>,
      IAuthorizationRequest;