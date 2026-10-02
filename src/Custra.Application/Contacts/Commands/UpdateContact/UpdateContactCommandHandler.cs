using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Contacts.Commands.UpdateContact;

public sealed class UpdateContactCommandHandler(
    IContactCommands contactCommands,
    IApplicationDbContext dbContext)
    : ICommandHandler<UpdateContactCommand, Unit>
{
    private readonly IContactCommands _contactCommands = contactCommands;
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Unit> Handle(
        UpdateContactCommand request,
        CancellationToken cancellationToken)
    {
        var contact = await _contactCommands.FindContactAsync(
            request.Id,
            cancellationToken);

        if (contact is null)
            throw new KeyNotFoundException(
                "Contact was not found.");

        contact.Update(
            request.FirstName,
            request.LastName,
            request.JobTitle,
            request.Email,
            request.Phone);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}