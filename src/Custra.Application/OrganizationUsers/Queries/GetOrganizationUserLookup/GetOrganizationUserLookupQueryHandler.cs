using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.OrganizationUsers.Queries.GetOrganizationUserLookup;

public sealed class GetOrganizationUserLookupQueryHandler
    : IQueryHandler<
        GetOrganizationUserLookupQuery,
        IReadOnlyList<LookupItemDto>>
{
    private readonly IOrganizationUserQueries _userQueries;

    public GetOrganizationUserLookupQueryHandler(
        IOrganizationUserQueries userQueries)
    {
        _userQueries = userQueries;
    }

    public Task<IReadOnlyList<LookupItemDto>> Handle(
        GetOrganizationUserLookupQuery request,
        CancellationToken cancellationToken)
    {
        return _userQueries.GetLookupAsync(cancellationToken);
    }
}