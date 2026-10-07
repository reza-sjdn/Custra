using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Common.Models;
using Custra.Application.Customers.Queries.GetCustomerById;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Queries;

public sealed class CustomerQueries(CustraDbContext dbContext)
    : ICustomerQueries
{
    private readonly CustraDbContext _dbContext = dbContext;

    public async Task<CustomerDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Customers
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new CustomerDto(
                x.Id,
                x.Name,
                x.CreatedAt))
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<PagedResult<CustomerDto>> GetPagedAsync(
        string? search,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Customers
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();
            query = query.Where(x => x.Name.Contains(search));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new CustomerDto(
                x.Id,
                x.Name,
                x.CreatedAt))
            .ToListAsync(cancellationToken);

        return new PagedResult<CustomerDto>(
            items,
            page,
            pageSize,
            totalCount);
    }

    public async Task<bool> ExistsAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Customers
            .AsNoTracking()
            .AnyAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<IReadOnlyList<LookupItemDto>> GetLookupAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Customers
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new LookupItemDto(
                x.Id,
                x.Name))
            .ToListAsync(cancellationToken);
    }

}