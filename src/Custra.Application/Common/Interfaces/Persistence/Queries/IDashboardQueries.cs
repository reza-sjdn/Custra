using Custra.Application.Dashboard.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Interfaces.Persistence.Queries;

public interface IDashboardQueries
{
    Task<DashboardSummaryDto> GetSummaryAsync(
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<LeadStatusCountDto>> GetLeadsByStatusAsync(
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<OpportunityStageCountDto>> GetOpportunitiesByStageAsync(
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<DashboardActivityDto>> GetRecentActivitiesAsync(
        int count = 10,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<DashboardActivityDto>> GetUpcomingActivitiesAsync(
        int count = 10,
        CancellationToken cancellationToken = default);
}