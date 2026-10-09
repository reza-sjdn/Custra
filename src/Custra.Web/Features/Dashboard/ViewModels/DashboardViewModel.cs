using Custra.Application.Dashboard.DTOs;

namespace Custra.Web.Features.Dashboard.ViewModels;

public sealed class DashboardViewModel
{
    public DashboardSummaryDto Summary { get; init; } = default!;

    public IReadOnlyList<LeadStatusCountDto> LeadsByStatus { get; init; } = [];

    public IReadOnlyList<OpportunityStageCountDto> OpportunitiesByStage { get; init; } = [];

    public IReadOnlyList<DashboardActivityDto> RecentActivities { get; init; } = [];

    public IReadOnlyList<DashboardActivityDto> UpcomingActivities { get; init; } = [];
}