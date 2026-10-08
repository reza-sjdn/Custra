using Custra.Application.Activities.DTOs;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Common.Models;
using Custra.Domain.Activities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Queries;

public sealed class ActivityQueries : IActivityQueries
{
    private readonly CustraDbContext _dbContext;

    public ActivityQueries(CustraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ActivityDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await (
            from activity in _dbContext.Activities.AsNoTracking()

            join owner in _dbContext.Users
                on activity.OwnerUserId equals owner.Id

            join customer in _dbContext.Customers
                on activity.CustomerId equals customer.Id into customers
            from customer in customers.DefaultIfEmpty()

            join contact in _dbContext.Contacts
                on activity.ContactId equals contact.Id into contacts
            from contact in contacts.DefaultIfEmpty()

            join lead in _dbContext.Leads
                on activity.LeadId equals lead.Id into leads
            from lead in leads.DefaultIfEmpty()

            join opportunity in _dbContext.Opportunities
                on activity.OpportunityId equals opportunity.Id into opportunities
            from opportunity in opportunities.DefaultIfEmpty()

            where activity.Id == id

            select new ActivityDto(
                activity.Id,
                activity.Subject,
                activity.Description,
                activity.Type,
                activity.Status,
                activity.DueDate,
                activity.CompletedAt,
                activity.OwnerUserId,
                owner.UserName!,
                activity.CustomerId,
                customer != null ? customer.Name : null,
                activity.ContactId,
                contact != null
                    ? contact.FirstName + " " + contact.LastName
                    : null,
                activity.LeadId,
                lead != null
                    ? lead.FirstName + " " + lead.LastName
                    : null,
                activity.OpportunityId,
                opportunity != null
                    ? opportunity.Title
                    : null,
                activity.CreatedAt
            )
        ).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PagedResult<ActivityDto>> GetPagedAsync(
        string? search,
        ActivityStatus? status,
        ActivityType? type,
        Guid? ownerUserId,
        Guid? customerId,
        Guid? contactId,
        Guid? leadId,
        Guid? opportunityId,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query =
            from activity in _dbContext.Activities.AsNoTracking()

            join owner in _dbContext.Users
                on activity.OwnerUserId equals owner.Id

            join customer in _dbContext.Customers
                on activity.CustomerId equals customer.Id into customers
            from customer in customers.DefaultIfEmpty()

            join contact in _dbContext.Contacts
                on activity.ContactId equals contact.Id into contacts
            from contact in contacts.DefaultIfEmpty()

            join lead in _dbContext.Leads
                on activity.LeadId equals lead.Id into leads
            from lead in leads.DefaultIfEmpty()

            join opportunity in _dbContext.Opportunities
                on activity.OpportunityId equals opportunity.Id into opportunities
            from opportunity in opportunities.DefaultIfEmpty()

            select new
            {
                Activity = activity,
                OwnerName = owner.UserName!,
                CustomerName = customer != null ? customer.Name : null,
                ContactName = contact != null
                    ? contact.FirstName + " " + contact.LastName
                    : null,
                LeadName = lead != null
                    ? lead.FirstName + " " + lead.LastName
                    : null,
                OpportunityTitle = opportunity != null
                    ? opportunity.Title
                    : null
            };

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x =>
                x.Activity.Subject.Contains(search) ||
                (x.Activity.Description != null &&
                 x.Activity.Description.Contains(search)));
        }

        if (status.HasValue)
            query = query.Where(x => x.Activity.Status == status.Value);

        if (type.HasValue)
            query = query.Where(x => x.Activity.Type == type.Value);

        if (ownerUserId.HasValue)
            query = query.Where(x => x.Activity.OwnerUserId == ownerUserId.Value);

        if (customerId.HasValue)
            query = query.Where(x => x.Activity.CustomerId == customerId.Value);

        if (contactId.HasValue)
            query = query.Where(x => x.Activity.ContactId == contactId.Value);

        if (leadId.HasValue)
            query = query.Where(x => x.Activity.LeadId == leadId.Value);

        if (opportunityId.HasValue)
            query = query.Where(x => x.Activity.OpportunityId == opportunityId.Value);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(x => x.Activity.DueDate)
            .ThenByDescending(x => x.Activity.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new ActivityDto(
                x.Activity.Id,
                x.Activity.Subject,
                x.Activity.Description,
                x.Activity.Type,
                x.Activity.Status,
                x.Activity.DueDate,
                x.Activity.CompletedAt,
                x.Activity.OwnerUserId,
                x.OwnerName,
                x.Activity.CustomerId,
                x.CustomerName,
                x.Activity.ContactId,
                x.ContactName,
                x.Activity.LeadId,
                x.LeadName,
                x.Activity.OpportunityId,
                x.OpportunityTitle,
                x.Activity.CreatedAt
            ))
            .ToListAsync(cancellationToken);

        return new PagedResult<ActivityDto>(
            items,
            totalCount,
            page,
            pageSize);
    }
}