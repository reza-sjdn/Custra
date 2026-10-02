using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Domain.Contacts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Contacts.Commands.CreateContact;

public sealed class CreateContactCommandHandler(
    IApplicationDbContext dbContext,
    IContactCommands contactCommands,
    ICustomerCommands customerCommands)
    : ICommandHandler<CreateContactCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly IContactCommands _contactCommands = contactCommands;
    private readonly ICustomerCommands _customerCommands = customerCommands;

    public async Task<Guid> Handle(
        CreateContactCommand request,
        CancellationToken cancellationToken)
    {
        var customer = await _customerCommands.FindCustomerAsync(
            request.CustomerId,
            cancellationToken);

        if (customer is null)
            throw new KeyNotFoundException(
                "Customer was not found.");

        var contact = new Contact(
            request.CustomerId,
            request.FirstName,
            request.LastName,
            request.JobTitle,
            request.Email,
            request.Phone);

        await _contactCommands.AddContactAsync(
            contact,
            cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return contact.Id;
    }
}