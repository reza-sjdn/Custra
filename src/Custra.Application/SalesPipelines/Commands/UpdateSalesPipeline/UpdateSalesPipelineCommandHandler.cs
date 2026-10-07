using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.SalesPipelines.Commands.UpdateSalesPipeline;

public sealed class UpdateSalesPipelineCommandHandler
    : ICommandHandler<UpdateSalesPipelineCommand, Unit>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ISalesPipelineCommands _commands;

    public UpdateSalesPipelineCommandHandler(
        IApplicationDbContext dbContext,
        ISalesPipelineCommands commands)
    {
        _dbContext = dbContext;
        _commands = commands;
    }

    public async Task<Unit> Handle(
        UpdateSalesPipelineCommand request,
        CancellationToken cancellationToken)
    {
        var pipeline = await _commands.FindAsync(
            request.Id,
            cancellationToken);

        if (pipeline is null)
            throw new KeyNotFoundException(
                "Sales pipeline was not found.");

        pipeline.UpdateName(request.Name);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}