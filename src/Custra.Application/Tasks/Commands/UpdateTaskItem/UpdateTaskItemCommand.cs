using Custra.Application.Common.Authorization;
using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using Custra.Domain.Authorization;
using Custra.Domain.Tasks;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tasks.Commands.UpdateTaskItem;

public sealed record UpdateTaskItemCommand(
    Guid Id,
    string Title,
    string? Description,
    TaskItemPriority Priority,
    DateTime? DueDate,
    Guid OwnerUserId,
    Guid? CustomerId,
    Guid? ContactId,
    Guid? LeadId,
    Guid? OpportunityId,
    Guid? ActivityId)
    : ICommand<Unit>, IAuthorizationRequest
{
}