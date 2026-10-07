using Custra.Domain.Opportunities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Interfaces.Persistence.Commands;

public interface ISalesPipelineCommands
{
    Task AddAsync(
        SalesPipeline pipeline,
        CancellationToken cancellationToken = default);

    Task<SalesPipeline?> FindAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        SalesPipeline pipeline,
        CancellationToken cancellationToken = default);
}