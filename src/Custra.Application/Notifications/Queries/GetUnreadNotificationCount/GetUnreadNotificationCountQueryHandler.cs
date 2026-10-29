using Custra.Application.Common.Interfaces.Persistence.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Notifications.Queries.GetUnreadNotificationCount;

public class GetUnreadNotificationCountQueryHandler
    : IRequestHandler<GetUnreadNotificationCountQuery, int>
{
    private readonly INotificationQueries _queries;

    public GetUnreadNotificationCountQueryHandler(
        INotificationQueries queries)
    {
        _queries = queries;
    }

    public Task<int> Handle(
        GetUnreadNotificationCountQuery request,
        CancellationToken cancellationToken)
    {
        return _queries.GetUnreadCountAsync(cancellationToken);
    }
}