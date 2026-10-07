using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Domain.Opportunities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Commands;

public sealed class SalesPipelineCommands : ISalesPipelineCommands
{
    private readonly CustraDbContext _dbContext;

    public SalesPipelineCommands(CustraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task AddAsync(
        SalesPipeline pipeline,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.SalesPipelines
            .AddAsync(pipeline, cancellationToken)
            .AsTask();
    }

    public Task<SalesPipeline?> FindAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.SalesPipelines
            .Include(x => x.Stages)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public Task DeleteAsync(
        SalesPipeline pipeline,
        CancellationToken cancellationToken = default)
    {
        _dbContext.SalesPipelines.Remove(pipeline);

        return Task.CompletedTask;
    }
}