using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Dashboard.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Dashboard.Queries.GetDashboard;

public sealed class GetDashboardQueryHandler
    : IQueryHandler<GetDashboardQuery, DashboardDto>
{
    private readonly IDashboardQueries _dashboardQueries;

    public GetDashboardQueryHandler(IDashboardQueries dashboardQueries)
    {
        _dashboardQueries = dashboardQueries;
    }

    public async Task<DashboardDto> Handle(
        GetDashboardQuery request,
        CancellationToken cancellationToken)
    {
        var summary = await _dashboardQueries.GetSummaryAsync(cancellationToken);
        var leads = await _dashboardQueries.GetLeadsByStatusAsync(cancellationToken);
        var stages = await _dashboardQueries.GetOpportunitiesByStageAsync(cancellationToken);
        var recent = await _dashboardQueries.GetRecentActivitiesAsync(
            cancellationToken: cancellationToken);
        var upcoming = await _dashboardQueries.GetUpcomingActivitiesAsync(
            cancellationToken: cancellationToken);

        return new DashboardDto(summary, leads, stages, recent, upcoming);
    }
}
