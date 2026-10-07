using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Domain.Leads;
using Custra.Domain.Opportunities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.SalesPipelines.Commands.CreateSalesPipeline;

public sealed class CreateSalesPipelineCommandHandler
    : ICommandHandler<CreateSalesPipelineCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ISalesPipelineCommands _salesPipelineCommands;

    public CreateSalesPipelineCommandHandler(
        IApplicationDbContext dbContext,
        ISalesPipelineCommands salesPipelineCommands)
    {
        _dbContext = dbContext;
        _salesPipelineCommands = salesPipelineCommands;
    }

    public async Task<Guid> Handle(
        CreateSalesPipelineCommand request,
        CancellationToken cancellationToken)
    {
        var pipeline = new SalesPipeline(request.Name);

        await _salesPipelineCommands.AddAsync(
            pipeline,
            cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return pipeline.Id;
    }
}