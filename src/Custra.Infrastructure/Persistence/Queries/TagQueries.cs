using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Common.Models;
using Custra.Application.Tags.DTOs;
using Custra.Domain.Tags;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Queries;


public class TagQueries : ITagQueries
{
    private readonly CustraDbContext _dbContext;

    public TagQueries(CustraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TagDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        int CustomerCount = _dbContext.CustomerTags
            .Count(x => x.TagId == id);

        int ContactCount = _dbContext.ContactTags
            .Count(x => x.TagId == id);

        int LeadCount = _dbContext.LeadTags
            .Count(x => x.TagId == id);

        int OpportunityCount = _dbContext.OpportunityTags
            .Count(x => x.TagId == id);

        int TotalUsageCount = CustomerCount + ContactCount +
                              LeadCount + OpportunityCount;

        return await _dbContext.Tags
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new TagDto(
                x.Id,
                x.Name,
                x.Color,
                x.CreatedAt,
                x.ModifiedAt,
                CustomerCount,
                ContactCount,
                LeadCount,
                OpportunityCount,
                TotalUsageCount))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PagedResult<TagDto>> GetPagedAsync(
        string? search = null,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var query = _dbContext.Tags.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();

            query = query.Where(x => x.Name.Contains(term));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(x => x.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new TagDto(
                x.Id,
                x.Name,
                x.Color,
                x.CreatedAt,
                x.ModifiedAt,
                null, null, null, null, null))
            .ToListAsync(cancellationToken);

        return new PagedResult<TagDto>(
            items, totalCount, page, pageSize);
    }

    public Task<bool> ExistsByNameAsync(
        string name,
        Guid? excludeId = null,
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Tags
            .Where(x => x.Name == name.Trim());

        if (excludeId.HasValue)
            query = query.Where(x => x.Id != excludeId.Value);

        return query.AnyAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<LookupItemDto>> GetLookupAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Tags
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new LookupItemDto(x.Id, x.Name))
            .ToListAsync(cancellationToken);
    }

}
