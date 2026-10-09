using Custra.Application.Common.Authorization;
using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Models;
using Custra.Application.Tasks.DTOs;
using Custra.Domain.Authorization;
using Custra.Domain.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tasks.Queries.GetTaskItems;

public sealed record GetTaskItemsQuery(
    string? Search = null,
    TaskItemStatus? Status = null,
    TaskItemPriority? Priority = null,
    Guid? OwnerUserId = null,
    Guid? CustomerId = null,
    Guid? ContactId = null,
    Guid? LeadId = null,
    Guid? OpportunityId = null,
    int Page = 1,
    int PageSize = 10)
    : IQuery<PagedResult<TaskItemDto>>, IAuthorizationRequest
{
}