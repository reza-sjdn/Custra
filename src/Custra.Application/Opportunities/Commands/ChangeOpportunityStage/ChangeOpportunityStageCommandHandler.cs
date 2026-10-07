using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Opportunities.Commands.ChangeOpportunityStage;

public sealed class ChangeOpportunityStageCommandHandler
    : ICommandHandler<ChangeOpportunityStageCommand, Unit>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IOpportunityCommands _opportunitycommands;
    private readonly IOpportunityQueries _opportunityQueries;

    public ChangeOpportunityStageCommandHandler(
        IApplicationDbContext dbContext,
        IOpportunityCommands opportunitycommands,
        IOpportunityQueries opportunityQueries)
    {
        _dbContext = dbContext;
        _opportunitycommands = opportunitycommands;
        _opportunityQueries = opportunityQueries;
    }

    public async Task<Unit> Handle(
        ChangeOpportunityStageCommand request,
        CancellationToken cancellationToken)
    {
        var opportunity = await _opportunitycommands.FindAsync(
            request.Id,
            cancellationToken);

        if (opportunity is null)
            throw new KeyNotFoundException(
                "Opportunity was not found.");

        var stageExists = await _opportunityQueries.StageBelongsToPipelineAsync(
            request.SalesPipelineStageId,
            opportunity.SalesPipelineId,
            cancellationToken);

        if (!stageExists)
            throw new KeyNotFoundException(
                "Sales pipeline stage was not found.");

        opportunity.ChangeStage(
            request.SalesPipelineStageId);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}