using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Domain.Opportunities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Commands;

public sealed class OpportunityCommands : IOpportunityCommands
{
    private readonly CustraDbContext _dbContext;

    public OpportunityCommands(CustraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task AddAsync(
        Opportunity opportunity,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.Opportunities.AddAsync(
            opportunity,
            cancellationToken).AsTask();
    }

    public Task<Opportunity?> FindAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.Opportunities
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task DeleteAsync(
        Opportunity opportunity,
        CancellationToken cancellationToken = default)
    {
        _dbContext.Opportunities.Remove(opportunity);

        return Task.CompletedTask;
    }
}