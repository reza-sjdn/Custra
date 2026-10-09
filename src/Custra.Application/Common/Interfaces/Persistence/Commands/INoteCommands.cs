using Custra.Domain.Notes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Interfaces.Persistence.Commands;


public interface INoteCommands
{
    Task AddAsync(
        Note note,
        CancellationToken cancellationToken = default);

    Task<Note?> FindAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    void Delete(Note note);
}