using Custra.Domain.Tasks;

namespace Custra.Web.Features.Tasks.ViewModels;

public sealed class TaskItemViewModel
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }

    public TaskItemStatus Status { get; init; }
    public TaskItemPriority Priority { get; init; }

    public DateTime? DueDate { get; init; }
    public DateTime? CompletedAt { get; init; }

    public Guid OwnerUserId { get; init; }
    public string OwnerName { get; init; } = string.Empty;

    public Guid? CustomerId { get; init; }
    public string? CustomerName { get; init; }

    public Guid? ContactId { get; init; }
    public string? ContactName { get; init; }

    public Guid? LeadId { get; init; }
    public string? LeadName { get; init; }

    public Guid? OpportunityId { get; init; }
    public string? OpportunityTitle { get; init; }

    public Guid? ActivityId { get; init; }
    public string? ActivitySubject { get; init; }

    public DateTime CreatedAt { get; init; }
}