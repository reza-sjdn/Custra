using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Domain.Tags;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Commands;


public class TagCommands : ITagCommands
{
    private readonly CustraDbContext _dbContext;

    public TagCommands(CustraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(
        Tag tag,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Tags.AddAsync(tag, cancellationToken);
    }

    public Task<Tag?> FindAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.Tags
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public void Delete(Tag tag)
    {
        _dbContext.Tags.Remove(tag);
    }
}
