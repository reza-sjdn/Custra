using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Common.Models;
using Custra.Application.Contacts.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Queries;

public sealed class ContactQueries(CustraDbContext dbContext)
    : IContactQueries
{
    private readonly CustraDbContext _dbContext = dbContext;

    public async Task<ContactDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Contacts
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new ContactDto(
                x.Id,
                x.CustomerId,
                x.FirstName,
                x.LastName,
                x.JobTitle,
                x.Email,
                x.Phone))
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<PagedResult<ContactDto>> GetPagedAsync(
        Guid customerId,
        string? search,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Contacts
            .AsNoTracking()
            .Where(x => x.CustomerId == customerId);

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();

            query = query.Where(x =>
                x.FirstName.Contains(search) ||
                x.LastName.Contains(search) ||
                (x.Email != null && x.Email.Contains(search)));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(x => x.LastName)
            .ThenBy(x => x.FirstName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new ContactDto(
                x.Id,
                x.CustomerId,
                x.FirstName,
                x.LastName,
                x.JobTitle,
                x.Email,
                x.Phone))
            .ToListAsync(cancellationToken);

        return new PagedResult<ContactDto>(
            items,
            page,
            pageSize,
            totalCount);
    }

    public async Task<IReadOnlyList<LookupItemDto>> GetLookupByCustomerIdAsync(
        Guid customerId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Contacts
            .AsNoTracking()
            .Where(x => x.CustomerId == customerId)
            .OrderBy(x => x.LastName)
            .ThenBy(x => x.FirstName)
            .Select(x => new LookupItemDto(
                x.Id,
                x.FirstName + " " + x.LastName))
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> BelongsToCustomerAsync(
        Guid contactId,
        Guid customerId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Contacts
            .AsNoTracking()
            .AnyAsync(
                x => x.Id == contactId && x.CustomerId == customerId,
                cancellationToken);
    }

}