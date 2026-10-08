using Custra.Application.Activities.DTOs;
using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Models;
using Custra.Domain.Activities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Activities.Queries.GetActivities;

public sealed record GetActivitiesQuery(
    string? Search = null,
    ActivityStatus? Status = null,
    ActivityType? Type = null,
    Guid? OwnerUserId = null,
    Guid? CustomerId = null,
    Guid? ContactId = null,
    Guid? LeadId = null,
    Guid? OpportunityId = null,
    int Page = 1,
    int PageSize = 10)
    : IQuery<PagedResult<ActivityDto>>,
      IAuthorizationRequest;