using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Common.Models;
using Custra.Application.Contacts.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Contacts.Queries.GetContacts;

public sealed class GetContactsQueryHandler(
    IContactQueries contactQueries)
    : IQueryHandler<GetContactsQuery, PagedResult<ContactDto>>
{
    private readonly IContactQueries _contactQueries = contactQueries;

    public async Task<PagedResult<ContactDto>> Handle(
        GetContactsQuery request,
        CancellationToken cancellationToken)
    {
        return await _contactQueries.GetPagedAsync(
            request.CustomerId,
            request.Search,
            request.Page,
            request.PageSize,
            cancellationToken);
    }
}