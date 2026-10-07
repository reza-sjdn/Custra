using Custra.Domain.Opportunities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Interfaces.Persistence.Commands;

public interface ISalesPipelineStageCommands
{
    Task AddAsync(
        SalesPipelineStage stage,
        CancellationToken cancellationToken = default);

    Task<SalesPipelineStage?> FindAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        SalesPipelineStage stage,
        CancellationToken cancellationToken = default);
}