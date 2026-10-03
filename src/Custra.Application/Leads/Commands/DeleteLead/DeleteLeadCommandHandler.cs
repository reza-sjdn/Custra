using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Leads.Commands.DeleteLead;

public sealed class DeleteLeadCommandHandler(
    ILeadCommands leadCommands,
    IApplicationDbContext dbContext)
    : ICommandHandler<DeleteLeadCommand, Unit>
{
    private readonly ILeadCommands _leadCommands = leadCommands;
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Unit> Handle(
        DeleteLeadCommand request,
        CancellationToken cancellationToken)
    {
        var lead = await _leadCommands.FindAsync(
            request.Id,
            cancellationToken);

        if (lead is null)
            throw new KeyNotFoundException(
                "Lead was not found.");

        await _leadCommands.DeleteAsync(
            lead,
            cancellationToken);

        await _dbContext.SaveChangesAsync(
            cancellationToken);

        return Unit.Value;
    }
}