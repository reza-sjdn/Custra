using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Common.Models;
using Custra.Application.SalesPipelines.DTOs;
using Custra.Application.SalesPipelineStages.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Queries;

public sealed class SalesPipelineQueries : ISalesPipelineQueries
{
    private readonly CustraDbContext _dbContext;

    public SalesPipelineQueries(CustraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SalesPipelineDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.SalesPipelines
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new SalesPipelineDto(
                x.Id,
                x.Name,
                x.Stages
                    .OrderBy(s => s.Order)
                    .Select(s => new SalesPipelineStageDto(
                        s.Id,
                        s.SalesPipelineId,
                        s.Name,
                        s.Order,
                        s.Probability))
                    .ToList()))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<SalesPipelineDto>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.SalesPipelines
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new SalesPipelineDto(
                x.Id,
                x.Name,
                x.Stages
                    .OrderBy(s => s.Order)
                    .Select(s => new SalesPipelineStageDto(
                        s.Id,
                        s.SalesPipelineId,
                        s.Name,
                        s.Order,
                        s.Probability))
                    .ToList()))
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<LookupItemDto>> GetLookupAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.SalesPipelines
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new LookupItemDto(
                x.Id,
                x.Name))
            .ToListAsync(cancellationToken);
    }

}