using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Domain.Activities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Commands;

public sealed class ActivityCommands : IActivityCommands
{
    private readonly CustraDbContext _dbContext;

    public ActivityCommands(CustraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(
        Activity activity,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Activities.AddAsync(
            activity,
            cancellationToken);
    }

    public async Task<Activity?> FindAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Activities
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public void Delete(Activity activity)
    {
        _dbContext.Activities.Remove(activity);
    }
}