using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Common.Models;
using Custra.Application.Tasks.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tasks.Queries.GetTaskItems;

public sealed class GetTaskItemsQueryHandler
    : IRequestHandler<GetTaskItemsQuery, PagedResult<TaskItemDto>>
{
    private readonly ITaskItemQueries _queries;

    public GetTaskItemsQueryHandler(ITaskItemQueries queries)
    {
        _queries = queries;
    }

    public Task<PagedResult<TaskItemDto>> Handle(
        GetTaskItemsQuery request,
        CancellationToken cancellationToken)
    {
        return _queries.GetPagedAsync(
            search: request.Search,
            status: request.Status,
            priority: request.Priority,
            ownerUserId: request.OwnerUserId,
            customerId: request.CustomerId,
            contactId: request.ContactId,
            leadId: request.LeadId,
            opportunityId: request.OpportunityId,
            page: request.Page,
            pageSize: request.PageSize,
            cancellationToken: cancellationToken);
    }
}