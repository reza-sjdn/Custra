using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Opportunities.Commands.DeleteOpportunity;

public sealed class DeleteOpportunityCommandHandler
    : ICommandHandler<DeleteOpportunityCommand, Unit>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IOpportunityCommands _commands;

    public DeleteOpportunityCommandHandler(
        IApplicationDbContext dbContext,
        IOpportunityCommands commands)
    {
        _dbContext = dbContext;
        _commands = commands;
    }

    public async Task<Unit> Handle(
        DeleteOpportunityCommand request,
        CancellationToken cancellationToken)
    {
        var opportunity = await _commands.FindAsync(
            request.Id,
            cancellationToken);

        if (opportunity is null)
            throw new KeyNotFoundException(
                "Opportunity was not found.");

        await _commands.DeleteAsync(
            opportunity,
            cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}