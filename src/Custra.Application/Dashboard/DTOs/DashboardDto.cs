using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Dashboard.DTOs;

public sealed record DashboardDto(
    DashboardSummaryDto Summary,
    IReadOnlyList<LeadStatusCountDto> LeadsByStatus,
    IReadOnlyList<OpportunityStageCountDto> OpportunitiesByStage,
    IReadOnlyList<DashboardActivityDto> RecentActivities,
    IReadOnlyList<DashboardActivityDto> UpcomingActivities);