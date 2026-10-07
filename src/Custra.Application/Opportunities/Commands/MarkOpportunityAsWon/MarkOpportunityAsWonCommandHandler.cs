using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Opportunities.Commands.MarkOpportunityAsWon;

public sealed class MarkOpportunityAsWonCommandHandler
    : ICommandHandler<MarkOpportunityAsWonCommand, Unit>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IOpportunityCommands _commands;

    public MarkOpportunityAsWonCommandHandler(
        IApplicationDbContext dbContext,
        IOpportunityCommands commands)
    {
        _dbContext = dbContext;
        _commands = commands;
    }

    public async Task<Unit> Handle(
        MarkOpportunityAsWonCommand request,
        CancellationToken cancellationToken)
    {
        var opportunity = await _commands.FindAsync(
            request.Id,
            cancellationToken);

        if (opportunity is null)
            throw new KeyNotFoundException(
                "Opportunity was not found.");

        opportunity.MarkAsWon();

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}