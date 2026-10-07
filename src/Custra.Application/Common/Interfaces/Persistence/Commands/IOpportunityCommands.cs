using Custra.Domain.Opportunities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Interfaces.Persistence.Commands;

public interface IOpportunityCommands
{
    Task AddAsync(
        Opportunity opportunity,
        CancellationToken cancellationToken = default);

    Task<Opportunity?> FindAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Opportunity opportunity,
        CancellationToken cancellationToken = default);
}