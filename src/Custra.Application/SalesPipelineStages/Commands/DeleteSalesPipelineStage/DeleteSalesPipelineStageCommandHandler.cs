using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.SalesPipelineStages.Commands.DeleteSalesPipelineStage;

public sealed class DeleteSalesPipelineStageCommandHandler
    : ICommandHandler<DeleteSalesPipelineStageCommand, Unit>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ISalesPipelineStageCommands _commands;
    private readonly IOpportunityQueries _opportunityQueries;

    public DeleteSalesPipelineStageCommandHandler(
        IApplicationDbContext dbContext,
        ISalesPipelineStageCommands commands,
        IOpportunityQueries opportunityQueries)
    {
        _dbContext = dbContext;
        _commands = commands;
        _opportunityQueries = opportunityQueries;
    }

    public async Task<Unit> Handle(
        DeleteSalesPipelineStageCommand request,
        CancellationToken cancellationToken)
    {
        var stage = await _commands.FindAsync(
            request.Id,
            cancellationToken);

        if (stage is null)
            throw new KeyNotFoundException(
                "Sales pipeline stage was not found.");

        var hasOpportunities = await _opportunityQueries
            .ExistsForPipelineAsync(
                request.Id,
                cancellationToken);

        if (hasOpportunities)
        {
            throw new InvalidOperationException(
                "A sales pipeline stage cannot be deleted while it contains opportunities.");
        }

        await _commands.DeleteAsync(
            stage,
            cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}