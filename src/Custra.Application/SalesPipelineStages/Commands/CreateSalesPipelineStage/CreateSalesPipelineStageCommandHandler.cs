using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Domain.Opportunities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.SalesPipelineStages.Commands.CreateSalesPipelineStage;

public sealed class CreateSalesPipelineStageCommandHandler
    : ICommandHandler<CreateSalesPipelineStageCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ISalesPipelineCommands _pipelineCommands;
    private readonly ISalesPipelineStageCommands _stageCommands;

    public CreateSalesPipelineStageCommandHandler(
        IApplicationDbContext dbContext,
        ISalesPipelineCommands pipelineCommands,
        ISalesPipelineStageCommands stageCommands)
    {
        _dbContext = dbContext;
        _pipelineCommands = pipelineCommands;
        _stageCommands = stageCommands;
    }

    public async Task<Guid> Handle(
        CreateSalesPipelineStageCommand request,
        CancellationToken cancellationToken)
    {
        var pipeline = await _pipelineCommands.FindAsync(
            request.SalesPipelineId, cancellationToken);

        if (pipeline is null)
            throw new KeyNotFoundException(
                "Sales pipeline was not found.");

        var stage = new SalesPipelineStage(
            request.SalesPipelineId,
            request.Name,
            request.Order,
            request.Probability);

        await _stageCommands.AddAsync(stage, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return stage.Id;
    }
}