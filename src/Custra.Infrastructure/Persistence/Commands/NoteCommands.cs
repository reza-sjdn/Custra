using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Domain.Notes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Commands;


public sealed class NoteCommands : INoteCommands
{
    private readonly CustraDbContext _dbContext;

    public NoteCommands(CustraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(
        Note note,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Notes.AddAsync(note, cancellationToken);
    }

    public async Task<Note?> FindAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Notes
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public void Delete(Note note)
    {
        _dbContext.Notes.Remove(note);
    }
}