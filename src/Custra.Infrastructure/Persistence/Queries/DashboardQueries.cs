using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Dashboard.DTOs;
using Custra.Domain.Activities;
using Custra.Domain.Opportunities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Queries;

public sealed class DashboardQueries : IDashboardQueries
{
    private readonly CustraDbContext _dbContext;

    public DashboardQueries(CustraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DashboardSummaryDto> GetSummaryAsync(
        CancellationToken cancellationToken = default)
    {
        var totalCustomers = await _dbContext.Customers
            .CountAsync(cancellationToken);

        var openOpportunities = _dbContext.Opportunities
            .Where(x => x.Status == OpportunityStatus.Open);

        var openOpportunityCount = await openOpportunities
            .CountAsync(cancellationToken);

        var pipelineValue = await openOpportunities
            .SumAsync(
                x => x.EstimatedValue ?? 0m,
                cancellationToken);

        var wonOpportunities = await _dbContext.Opportunities
            .CountAsync(
                x => x.Status == OpportunityStatus.Won,
                cancellationToken);

        var lostOpportunities = await _dbContext.Opportunities
            .CountAsync(
                x => x.Status == OpportunityStatus.Lost,
                cancellationToken);

        var openActivities = await _dbContext.Activities
            .CountAsync(
                x => x.Status == ActivityStatus.Planned,
                cancellationToken);

        var now = DateTime.Now;

        var overdueActivities = await _dbContext.Activities
            .CountAsync(
                x => x.Status == ActivityStatus.Planned
                     && x.DueDate.HasValue
                     && x.DueDate.Value < now,
                cancellationToken);

        return new DashboardSummaryDto(
            totalCustomers,
            openOpportunityCount,
            pipelineValue,
            wonOpportunities,
            lostOpportunities,
            openActivities,
            overdueActivities);
    }

    public async Task<IReadOnlyList<LeadStatusCountDto>> GetLeadsByStatusAsync(
        CancellationToken cancellationToken = default)
    {
        var results = await _dbContext.Leads
            .AsNoTracking()
            .GroupBy(x => x.Status)
            .Select(g => new
            {
                Status = g.Key,
                Count = g.Count()
            })
            .ToListAsync(cancellationToken);

        return results
            .OrderBy(x => x.Status)
            .Select(x => new LeadStatusCountDto(x.Status, x.Count))
            .ToList();
    }

    public async Task<IReadOnlyList<OpportunityStageCountDto>>
        GetOpportunitiesByStageAsync(
            CancellationToken cancellationToken = default)
    {
        return await (
            from opportunity in _dbContext.Opportunities.AsNoTracking()
            join stage in _dbContext.SalesPipelineStages.AsNoTracking()
                on opportunity.SalesPipelineStageId equals stage.Id
            where opportunity.Status == OpportunityStatus.Open
            group opportunity by new
            {
                stage.Id,
                stage.Name,
                stage.Order
            }
            into stageGroup
            orderby stageGroup.Key.Order
            select new OpportunityStageCountDto(
                stageGroup.Key.Id,
                stageGroup.Key.Name,
                stageGroup.Count(),
                stageGroup.Sum(x => x.EstimatedValue ?? 0m))
        ).ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<DashboardActivityDto>>
        GetRecentActivitiesAsync(
            int count = 10,
            CancellationToken cancellationToken = default)
    {
        count = Math.Clamp(count, 1, 50);

        return await (
            from activity in _dbContext.Activities.AsNoTracking()
            join owner in _dbContext.Users.AsNoTracking()
                on activity.OwnerUserId equals owner.Id
            orderby activity.CreatedAt descending
            select new DashboardActivityDto(
                activity.Id,
                activity.Subject,
                activity.Type,
                activity.Status,
                activity.DueDate,
                owner.UserName!
            )
        )
        .Take(count)
        .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<DashboardActivityDto>>
        GetUpcomingActivitiesAsync(
            int count = 10,
            CancellationToken cancellationToken = default)
    {
        count = Math.Clamp(count, 1, 50);

        var now = DateTime.Now;

        return await (
            from activity in _dbContext.Activities.AsNoTracking()
            join owner in _dbContext.Users.AsNoTracking()
                on activity.OwnerUserId equals owner.Id
            where activity.Status == ActivityStatus.Planned
                  && activity.DueDate.HasValue
                  && activity.DueDate.Value >= now
            orderby activity.DueDate
            select new DashboardActivityDto(
                activity.Id,
                activity.Subject,
                activity.Type,
                activity.Status,
                activity.DueDate,
                owner.UserName!
            )
        )
        .Take(count)
        .ToListAsync(cancellationToken);
    }
}