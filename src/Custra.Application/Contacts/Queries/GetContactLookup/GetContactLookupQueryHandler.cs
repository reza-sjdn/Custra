using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Contacts.Queries.GetContactLookup;

public sealed class GetContactLookupQueryHandler
    : IQueryHandler<
        GetContactLookupQuery,
        IReadOnlyList<LookupItemDto>>
{
    private readonly IContactQueries _contactQueries;

    public GetContactLookupQueryHandler(
        IContactQueries contactQueries)
    {
        _contactQueries = contactQueries;
    }

    public Task<IReadOnlyList<LookupItemDto>> Handle(
        GetContactLookupQuery request,
        CancellationToken cancellationToken)
    {
        return _contactQueries.GetLookupByCustomerIdAsync(
            request.CustomerId,
            cancellationToken);
    }
}