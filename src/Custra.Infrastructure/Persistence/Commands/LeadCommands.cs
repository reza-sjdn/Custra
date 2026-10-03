using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Domain.Leads;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Commands;

public sealed class LeadCommands(CustraDbContext dbContext)
    : ILeadCommands
{
    private readonly CustraDbContext _dbContext = dbContext;

    public async Task AddAsync(
        Lead lead,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Leads.AddAsync(
            lead,
            cancellationToken);
    }

    public async Task<Lead?> FindAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Leads
            .SingleOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public Task DeleteAsync(
        Lead lead,
        CancellationToken cancellationToken = default)
    {
        _dbContext.Leads.Remove(lead);
        return Task.CompletedTask;
    }
}