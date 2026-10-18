using Custra.Domain.Tags;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Interfaces.Persistence.Commands;


public interface ITagCommands
{
    Task AddAsync(Tag tag, CancellationToken cancellationToken = default);

    Task<Tag?> FindAsync(Guid id, CancellationToken cancellationToken = default);

    void Delete(Tag tag);
}
