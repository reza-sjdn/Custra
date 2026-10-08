using Custra.Application.Activities.DTOs;
using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Activities.Queries.GetActivities;

public sealed class GetActivitiesQueryHandler
    : IQueryHandler<GetActivitiesQuery, PagedResult<ActivityDto>>
{
    private readonly IActivityQueries _activityQueries;

    public GetActivitiesQueryHandler(
        IActivityQueries activityQueries)
    {
        _activityQueries = activityQueries;
    }

    public async Task<PagedResult<ActivityDto>> Handle(
        GetActivitiesQuery request,
        CancellationToken cancellationToken)
    {
        return await _activityQueries.GetPagedAsync(
            request.Search,
            request.Status,
            request.Type,
            request.OwnerUserId,
            request.CustomerId,
            request.ContactId,
            request.LeadId,
            request.OpportunityId,
            request.Page,
            request.PageSize,
            cancellationToken);
    }
}