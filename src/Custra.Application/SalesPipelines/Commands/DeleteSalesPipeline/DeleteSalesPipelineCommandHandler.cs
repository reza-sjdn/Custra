using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.SalesPipelines.Commands.DeleteSalesPipeline;

public sealed class DeleteSalesPipelineCommandHandler
    : ICommandHandler<DeleteSalesPipelineCommand, Unit>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ISalesPipelineCommands _commands;
    private readonly IOpportunityQueries _opportunityQueries;

    public DeleteSalesPipelineCommandHandler(
        IApplicationDbContext dbContext,
        ISalesPipelineCommands commands,
        IOpportunityQueries opportunityQueries)
    {
        _dbContext = dbContext;
        _commands = commands;
        _opportunityQueries = opportunityQueries;
    }

    public async Task<Unit> Handle(
        DeleteSalesPipelineCommand request,
        CancellationToken cancellationToken)
    {
        var pipeline = await _commands.FindAsync(
            request.Id,
            cancellationToken);

        if (pipeline is null)
            throw new KeyNotFoundException(
                "Sales pipeline was not found.");

        var hasOpportunities = await _opportunityQueries
            .ExistsForPipelineAsync(
                request.Id,
                cancellationToken);

        if (hasOpportunities)
        {
            throw new InvalidOperationException(
                "A sales pipeline cannot be deleted while it contains opportunities.");
        }

        await _commands.DeleteAsync(
            pipeline,
            cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}