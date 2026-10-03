using Custra.Domain.Leads;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Interfaces.Persistence.Commands;

public interface ILeadCommands
{
    Task AddAsync(
        Lead lead,
        CancellationToken cancellationToken = default);

    Task<Lead?> FindAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Lead lead,
        CancellationToken cancellationToken = default);
}