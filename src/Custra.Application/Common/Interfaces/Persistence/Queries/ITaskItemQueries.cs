using Custra.Application.Common.Models;
using Custra.Application.Tasks.DTOs;
using Custra.Domain.Tasks;

namespace Custra.Application.Common.Interfaces.Persistence.Queries;

public interface ITaskItemQueries
{
    Task<TaskItemDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<PagedResult<TaskItemDto>> GetPagedAsync(
        string? search = null,
        TaskItemStatus? status = null,
        TaskItemPriority? priority = null,
        Guid? ownerUserId = null,
        Guid? customerId = null,
        Guid? contactId = null,
        Guid? leadId = null,
        Guid? opportunityId = null,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<LookupItemDto>> GetLookupAsync(
        CancellationToken cancellationToken = default);

}