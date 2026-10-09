using Custra.Domain.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Custra.Web.Features.Tasks.ViewModels;

public sealed class EditTaskItemViewModel
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }

    public TaskItemPriority Priority { get; set; }
    public DateTime? DueDate { get; set; }

    public Guid OwnerUserId { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? ContactId { get; set; }
    public Guid? LeadId { get; set; }
    public Guid? OpportunityId { get; set; }

    // Preserve the existing activity association until an activity lookup
    // is added to the UI.
    public Guid? ActivityId { get; set; }

    public IReadOnlyList<SelectListItem> Owners { get; set; } = [];
    public IReadOnlyList<SelectListItem> Customers { get; set; } = [];
    public IReadOnlyList<SelectListItem> Contacts { get; set; } = [];
    public IReadOnlyList<SelectListItem> Leads { get; set; } = [];
    public IReadOnlyList<SelectListItem> Opportunities { get; set; } = [];
}