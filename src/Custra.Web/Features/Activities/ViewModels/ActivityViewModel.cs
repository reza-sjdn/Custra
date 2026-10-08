using Custra.Domain.Activities;

namespace Custra.Web.Features.Activities.ViewModels;

public sealed class ActivityViewModel
{
    public Guid Id { get; set; }

    public string Subject { get; set; } = string.Empty;
    public string? Description { get; set; }

    public ActivityType Type { get; set; }
    public ActivityStatus Status { get; set; }

    public DateTime? DueDate { get; set; }
    public DateTime? CompletedAt { get; set; }

    public Guid OwnerUserId { get; set; }

    public Guid? CustomerId { get; set; }
    public Guid? ContactId { get; set; }
    public Guid? LeadId { get; set; }
    public Guid? OpportunityId { get; set; }

    public string OwnerName { get; init; } = string.Empty;
    public string? CustomerName { get; init; }
    public string? ContactName { get; init; }
    public string? LeadName { get; init; }
    public string? OpportunityTitle { get; init; }

    public DateTime CreatedAt { get; set; }
}