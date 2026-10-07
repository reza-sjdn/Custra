using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.SalesPipelineStages.Commands.UpdateSalesPipelineStage;

public sealed class UpdateSalesPipelineStageCommandHandler
    : ICommandHandler<UpdateSalesPipelineStageCommand, Unit>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ISalesPipelineStageCommands _commands;

    public UpdateSalesPipelineStageCommandHandler(
        IApplicationDbContext dbContext,
        ISalesPipelineStageCommands commands)
    {
        _dbContext = dbContext;
        _commands = commands;
    }

    public async Task<Unit> Handle(
        UpdateSalesPipelineStageCommand request,
        CancellationToken cancellationToken)
    {
        var stage = await _commands.FindAsync(
            request.Id,
            cancellationToken);

        if (stage is null)
            throw new KeyNotFoundException(
                "Sales pipeline stage was not found.");

        stage.Update(
            request.Name,
            request.Order,
            request.Probability);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}