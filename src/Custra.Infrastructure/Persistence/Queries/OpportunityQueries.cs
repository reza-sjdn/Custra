using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Common.Models;
using Custra.Application.Opportunities.DTOs;
using Custra.Domain.Opportunities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Queries;

public sealed class OpportunityQueries : IOpportunityQueries
{
    private readonly CustraDbContext _dbContext;

    public OpportunityQueries(CustraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OpportunityDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Opportunities
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new OpportunityDto(
                x.Id,
                x.CustomerId,
                x.ContactId,
                x.OwnerUserId,
                x.SalesPipelineId,
                x.SalesPipelineStageId,
                x.Title,
                x.Description,
                x.EstimatedValue,
                x.ExpectedCloseDate,
                x.Status,
                x.CreatedAt))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PagedResult<OpportunityDto>> GetPagedAsync(
        string? search,
        OpportunityStatus? status,
        Guid? customerId,
        Guid? ownerUserId,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Opportunities
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();

            query = query.Where(x =>
                x.Title.Contains(search) ||
                (x.Description != null &&
                 x.Description.Contains(search)));
        }

        if (status.HasValue)
        {
            query = query.Where(x =>
                x.Status == status.Value);
        }

        if (customerId.HasValue)
        {
            query = query.Where(x =>
                x.CustomerId == customerId.Value);
        }

        if (ownerUserId.HasValue)
        {
            query = query.Where(x =>
                x.OwnerUserId == ownerUserId.Value);
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new OpportunityDto(
                x.Id,
                x.CustomerId,
                x.ContactId,
                x.OwnerUserId,
                x.SalesPipelineId,
                x.SalesPipelineStageId,
                x.Title,
                x.Description,
                x.EstimatedValue,
                x.ExpectedCloseDate,
                x.Status,
                x.CreatedAt))
            .ToListAsync(cancellationToken);

        return new PagedResult<OpportunityDto>(
            items,
            page,
            pageSize,
            totalCount);
    }

    public async Task<bool> ExistsForPipelineAsync(
        Guid salesPipelineId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Opportunities
            .AsNoTracking()
            .AnyAsync(
                x => x.SalesPipelineId == salesPipelineId,
                cancellationToken);
    }

    public async Task<bool> StageBelongsToPipelineAsync(
        Guid stageId,
        Guid pipelineId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.SalesPipelineStages
            .AsNoTracking()
            .AnyAsync(
                x => x.Id == stageId &&
                     x.SalesPipelineId == pipelineId,
                cancellationToken);
    }

}