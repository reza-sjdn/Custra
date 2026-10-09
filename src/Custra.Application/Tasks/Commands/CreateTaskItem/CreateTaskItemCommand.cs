using Custra.Application.Common.Authorization;
using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using Custra.Domain.Authorization;
using Custra.Domain.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tasks.Commands.CreateTaskItem;

public sealed record CreateTaskItemCommand(
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
    : ICommand<Guid>, IAuthorizationRequest
{
}