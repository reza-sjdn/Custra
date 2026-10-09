using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Common.Models;
using Custra.Application.Notes.DTOs;
using Custra.Domain.Notes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Queries;


public sealed class NoteQueries : INoteQueries
{
    private readonly CustraDbContext _dbContext;

    public NoteQueries(CustraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<NoteDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await BuildQuery()
            .Where(x => x.Id == id)
            .Select(x => new NoteDto(
                x.Id,
                x.Title,
                x.Content,
                x.CustomerId,
                x.Customer == null ? null : x.Customer.Name,
                x.ContactId,
                x.Contact == null
                    ? null
                    : x.Contact.FirstName + " " + x.Contact.LastName,
                x.LeadId,
                x.Lead == null
                    ? null
                    : x.Lead.FirstName + " " + x.Lead.LastName,
                x.OpportunityId,
                x.Opportunity == null ? null : x.Opportunity.Title,
                x.CreatedAt,
                x.ModifiedAt))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PagedResult<NoteDto>> GetPagedAsync(
        string? search = null,
        Guid? customerId = null,
        Guid? contactId = null,
        Guid? leadId = null,
        Guid? opportunityId = null,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var query = BuildQuery();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();

            query = query.Where(x =>
                x.Title.Contains(term) ||
                x.Content.Contains(term) ||
                (x.Customer != null && x.Customer.Name.Contains(term)) ||
                (x.Contact != null &&
                    (x.Contact.FirstName + " " + x.Contact.LastName).Contains(term)) ||
                (x.Lead != null &&
                    (x.Lead.FirstName + " " + x.Lead.LastName).Contains(term)) ||
                (x.Opportunity != null && x.Opportunity.Title.Contains(term)));
        }

        if (customerId.HasValue)
            query = query.Where(x => x.CustomerId == customerId.Value);

        if (contactId.HasValue)
            query = query.Where(x => x.ContactId == contactId.Value);

        if (leadId.HasValue)
            query = query.Where(x => x.LeadId == leadId.Value);

        if (opportunityId.HasValue)
            query = query.Where(x => x.OpportunityId == opportunityId.Value);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(x => x.CreatedAt)
            .ThenBy(x => x.Title)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new NoteDto(
                x.Id,
                x.Title,
                x.Content,
                x.CustomerId,
                x.Customer == null ? null : x.Customer.Name,
                x.ContactId,
                x.Contact == null
                    ? null
                    : x.Contact.FirstName + " " + x.Contact.LastName,
                x.LeadId,
                x.Lead == null
                    ? null
                    : x.Lead.FirstName + " " + x.Lead.LastName,
                x.OpportunityId,
                x.Opportunity == null ? null : x.Opportunity.Title,
                x.CreatedAt,
                x.ModifiedAt))
            .ToListAsync(cancellationToken);

        return new PagedResult<NoteDto>(
            items,
            totalCount,
            page,
            pageSize);
    }

    private IQueryable<Note> BuildQuery()
    {
        return _dbContext.Notes
            .AsNoTracking();
    }
}