using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Contacts.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Contacts.Queries.GetContactById;

public sealed class GetContactByIdQueryHandler(
    IContactQueries contactQueries)
    : IQueryHandler<GetContactByIdQuery, ContactDto>
{
    private readonly IContactQueries _contactQueries = contactQueries;

    public async Task<ContactDto> Handle(
        GetContactByIdQuery request,
        CancellationToken cancellationToken)
    {
        var contact = await _contactQueries.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (contact is null)
            throw new KeyNotFoundException(
                "Contact was not found.");

        return contact;
    }
}