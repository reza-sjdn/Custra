
using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Common.Models;
using Custra.Application.Tasks.DTOs;
using Custra.Domain.Tasks;
using Custra.Infrastructure.Identity;
using Custra.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public sealed class TaskItemQueries : ITaskItemQueries
{
    private readonly CustraDbContext _dbContext;

    public TaskItemQueries(CustraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TaskItemDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await BuildQuery()
            .Where(x => x.Task.Id == id)
            .Select(x => new TaskItemDto(
                x.Task.Id,
                x.Task.Title,
                x.Task.Description,
                x.Task.Status,
                x.Task.Priority,
                x.Task.DueDate,
                x.Task.CompletedAt,
                x.Task.OwnerUserId,
                x.Owner.UserName!,
                x.Task.CustomerId,
                x.Task.Customer == null ? null : x.Task.Customer.Name,
                x.Task.ContactId,
                x.Task.Contact == null
                    ? null
                    : x.Task.Contact.FirstName + " " + x.Task.Contact.LastName,
                x.Task.LeadId,
                x.Task.Lead == null
                    ? null
                    : x.Task.Lead.FirstName + " " + x.Task.Lead.LastName,
                x.Task.OpportunityId,
                x.Task.Opportunity == null ? null : x.Task.Opportunity.Title,
                x.Task.ActivityId,
                x.Task.Activity == null ? null : x.Task.Activity.Subject,
                x.Task.CreatedAt))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PagedResult<TaskItemDto>> GetPagedAsync(
        string? search = null,
        TaskItemStatus? status = null,
        TaskItemPriority? priority = null,
        Guid? ownerUserId = null,
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
                x.Task.Title.Contains(term) ||
                (x.Task.Description != null && x.Task.Description.Contains(term)) ||
                (x.Task.Customer != null && x.Task.Customer.Name.Contains(term)) ||
                (x.Task.Contact != null &&
                    (x.Task.Contact.FirstName + " " + x.Task.Contact.LastName).Contains(term)) ||
                (x.Task.Lead != null &&
                    (x.Task.Lead.FirstName + " " + x.Task.Lead.LastName).Contains(term)) ||
                (x.Task.Opportunity != null && x.Task.Opportunity.Title.Contains(term)) ||
                x.Owner.UserName!.Contains(term));
        }

        if (status.HasValue)
            query = query.Where(x => x.Task.Status == status.Value);

        if (priority.HasValue)
            query = query.Where(x => x.Task.Priority == priority.Value);

        if (ownerUserId.HasValue)
            query = query.Where(x => x.Task.OwnerUserId == ownerUserId.Value);

        if (customerId.HasValue)
            query = query.Where(x => x.Task.CustomerId == customerId.Value);

        if (contactId.HasValue)
            query = query.Where(x => x.Task.ContactId == contactId.Value);

        if (leadId.HasValue)
            query = query.Where(x => x.Task.LeadId == leadId.Value);

        if (opportunityId.HasValue)
            query = query.Where(x => x.Task.OpportunityId == opportunityId.Value);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(x => x.Task.DueDate.HasValue ? 0 : 1)
            .ThenBy(x => x.Task.DueDate)
            .ThenBy(x => x.Task.Title)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new TaskItemDto(
                x.Task.Id,
                x.Task.Title,
                x.Task.Description,
                x.Task.Status,
                x.Task.Priority,
                x.Task.DueDate,
                x.Task.CompletedAt,
                x.Task.OwnerUserId,
                x.Owner.UserName!,
                x.Task.CustomerId,
                x.Task.Customer == null ? null : x.Task.Customer.Name,
                x.Task.ContactId,
                x.Task.Contact == null
                    ? null
                    : x.Task.Contact.FirstName + " " + x.Task.Contact.LastName,
                x.Task.LeadId,
                x.Task.Lead == null
                    ? null
                    : x.Task.Lead.FirstName + " " + x.Task.Lead.LastName,
                x.Task.OpportunityId,
                x.Task.Opportunity == null ? null : x.Task.Opportunity.Title,
                x.Task.ActivityId,
                x.Task.Activity == null ? null : x.Task.Activity.Subject,
                x.Task.CreatedAt))
            .ToListAsync(cancellationToken);

        return new PagedResult<TaskItemDto>(
            items, totalCount, page, pageSize);
    }

    public async Task<IReadOnlyList<LookupItemDto>> GetLookupAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Tasks
            .AsNoTracking()
            .OrderBy(x => x.Title)
            .Select(x => new LookupItemDto(x.Id, x.Title))
            .ToListAsync(cancellationToken);
    }

    private sealed class TaskItemQueryRow
    {
        public TaskItem Task { get; init; } = null!;
        public ApplicationUser Owner { get; init; } = null!;
    }

    private IQueryable<TaskItemQueryRow> BuildQuery()
    {
        return
            from task in _dbContext.Tasks.AsNoTracking()
            join owner in _dbContext.Users.AsNoTracking()
                on task.OwnerUserId equals owner.Id
            select new TaskItemQueryRow
            {
                Task = task,
                Owner = owner
            };
    }
}