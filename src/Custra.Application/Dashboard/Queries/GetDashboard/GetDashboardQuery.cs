using Custra.Application.Common.Authorization;
using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Dashboard.DTOs;
using Custra.Domain.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Dashboard.Queries.GetDashboard;

public sealed record GetDashboardQuery
    : IQuery<DashboardDto>, IAuthorizationRequest
{
}