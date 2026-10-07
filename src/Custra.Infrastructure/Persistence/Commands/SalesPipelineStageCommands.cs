using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Domain.Opportunities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Commands;

public sealed class SalesPipelineStageCommands
    : ISalesPipelineStageCommands
{
    private readonly CustraDbContext _dbContext;

    public SalesPipelineStageCommands(
        CustraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task AddAsync(
        SalesPipelineStage stage,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.SalesPipelineStages
            .AddAsync(stage, cancellationToken)
            .AsTask();
    }

    public Task<SalesPipelineStage?> FindAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.SalesPipelineStages
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public Task DeleteAsync(
        SalesPipelineStage stage,
        CancellationToken cancellationToken = default)
    {
        _dbContext.SalesPipelineStages.Remove(stage);

        return Task.CompletedTask;
    }
}