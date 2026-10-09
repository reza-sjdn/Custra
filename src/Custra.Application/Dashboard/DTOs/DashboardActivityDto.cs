using Custra.Domain.Activities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Dashboard.DTOs;

public sealed record DashboardActivityDto(
    Guid Id,
    string Subject,
    ActivityType Type,
    ActivityStatus Status,
    DateTime? DueDate,
    string OwnerName);