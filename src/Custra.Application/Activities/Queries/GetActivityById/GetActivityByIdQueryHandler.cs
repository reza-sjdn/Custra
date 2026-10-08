using Custra.Application.Activities.DTOs;
using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Activities.Queries.GetActivityById;

public sealed class GetActivityByIdQueryHandler
    : IQueryHandler<GetActivityByIdQuery, ActivityDto?>
{
    private readonly IActivityQueries _activityQueries;

    public GetActivityByIdQueryHandler(
        IActivityQueries activityQueries)
    {
        _activityQueries = activityQueries;
    }

    public async Task<ActivityDto?> Handle(
        GetActivityByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _activityQueries.GetByIdAsync(
            request.Id,
            cancellationToken);
    }
}