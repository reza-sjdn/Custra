using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Dashboard.DTOs;

public sealed record DashboardSummaryDto(
    int TotalCustomers,
    int OpenOpportunities,
    decimal PipelineValue,
    int WonOpportunities,
    int LostOpportunities,
    int OpenActivities,
    int OverdueActivities);
