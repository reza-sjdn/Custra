using Custra.Application.Activities.DTOs;
using Custra.Application.Common.Models;
using Custra.Domain.Activities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Interfaces.Persistence.Queries;

public interface IActivityQueries
{
    Task<ActivityDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<PagedResult<ActivityDto>> GetPagedAsync(
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
        CancellationToken cancellationToken = default);
}