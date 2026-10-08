using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Common.Models;
using Custra.Application.Leads.DTOs;
using Custra.Domain.Leads;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Queries;

public sealed class LeadQueries(CustraDbContext dbContext)
    : ILeadQueries
{
    private readonly CustraDbContext _dbContext = dbContext;

    public async Task<LeadDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Leads
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new LeadDto(
                x.Id,
                x.FirstName,
                x.LastName,
                x.CompanyName,
                x.JobTitle,
                x.Email,
                x.Phone,
                x.Status,
                x.CreatedAt))
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<PagedResult<LeadDto>> GetPagedAsync(
        string? search,
        LeadStatus? status,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Leads
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();

            query = query.Where(x =>
                x.FirstName.Contains(search) ||
                x.LastName.Contains(search) ||
                (x.CompanyName != null &&
                 x.CompanyName.Contains(search)) ||
                (x.Email != null &&
                 x.Email.Contains(search)));
        }

        if (status.HasValue)
        {
            query = query.Where(x => x.Status == status.Value);
        }

        var totalCount =
            await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new LeadDto(
                x.Id,
                x.FirstName,
                x.LastName,
                x.CompanyName,
                x.JobTitle,
                x.Email,
                x.Phone,
                x.Status,
                x.CreatedAt))
            .ToListAsync(cancellationToken);

        return new PagedResult<LeadDto>(
            items,
            page,
            pageSize,
            totalCount);
    }

    public async Task<bool> ExistsAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Leads
            .AsNoTracking()
            .AnyAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<LookupItemDto>> GetLookupAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Leads
            .AsNoTracking()
            .OrderBy(x => x.FirstName)
            .ThenBy(x => x.LastName)
            .Select(x => new LookupItemDto(
                x.Id,
                x.FirstName + " " + x.LastName))
            .ToListAsync(cancellationToken);
    }

}