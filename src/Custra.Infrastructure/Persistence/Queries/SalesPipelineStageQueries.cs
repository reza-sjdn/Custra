using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Common.Models;
using Custra.Application.SalesPipelineStages.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Queries;

public sealed class SalesPipelineStageQueries
    : ISalesPipelineStageQueries
{
    private readonly CustraDbContext _dbContext;

    public SalesPipelineStageQueries(
        CustraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SalesPipelineStageDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.SalesPipelineStages
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new SalesPipelineStageDto(
                x.Id,
                x.SalesPipelineId,
                x.Name,
                x.Order,
                x.Probability))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<SalesPipelineStageDto>>
        GetByPipelineIdAsync(
            Guid salesPipelineId,
            CancellationToken cancellationToken = default)
    {
        return await _dbContext.SalesPipelineStages
            .AsNoTracking()
            .Where(x => x.SalesPipelineId == salesPipelineId)
            .OrderBy(x => x.Order)
            .Select(x => new SalesPipelineStageDto(
                x.Id,
                x.SalesPipelineId,
                x.Name,
                x.Order,
                x.Probability))
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<LookupItemDto>> GetLookupByPipelineIdAsync(
        Guid pipelineId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.SalesPipelineStages
            .AsNoTracking()
            .Where(x => x.SalesPipelineId == pipelineId)
            .OrderBy(x => x.Order)
            .Select(x => new LookupItemDto(
                x.Id,
                x.Name))
            .ToListAsync(cancellationToken);
    }

}