using Custra.Domain.Tasks;

namespace Custra.Application.Tasks.DTOs;

public sealed record TaskItemDto(
    Guid Id,
    string Title,
    string? Description,
    TaskItemStatus Status,
    TaskItemPriority Priority,
    DateTime? DueDate,
    DateTime? CompletedAt,
    Guid OwnerUserId,
    string OwnerName,
    Guid? CustomerId,
    string? CustomerName,
    Guid? ContactId,
    string? ContactName,
    Guid? LeadId,
    string? LeadName,
    Guid? OpportunityId,
    string? OpportunityTitle,
    Guid? ActivityId,
    string? ActivitySubject,
    DateTime CreatedAt);