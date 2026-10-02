using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Contacts.Commands.DeleteContact;

public sealed class DeleteContactCommandHandler(
    IContactCommands contactCommands,
    IApplicationDbContext dbContext)
    : ICommandHandler<DeleteContactCommand, Unit>
{
    private readonly IContactCommands _contactCommands = contactCommands;
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Unit> Handle(
        DeleteContactCommand request,
        CancellationToken cancellationToken)
    {
        var contact = await _contactCommands.FindContactAsync(
            request.Id,
            cancellationToken);

        if (contact is null)
            throw new KeyNotFoundException(
                "Contact was not found.");

        await _contactCommands.DeleteAsync(
            contact,
            cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}