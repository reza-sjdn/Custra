using Custra.Domain.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Custra.Web.Features.Tasks.ViewModels;

public sealed class CreateTaskItemViewModel
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }

    public TaskItemPriority Priority { get; set; } = TaskItemPriority.Normal;
    public DateTime? DueDate { get; set; }

    public Guid OwnerUserId { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? ContactId { get; set; }
    public Guid? LeadId { get; set; }
    public Guid? OpportunityId { get; set; }

    public IReadOnlyList<SelectListItem> Owners { get; set; } = [];
    public IReadOnlyList<SelectListItem> Customers { get; set; } = [];
    public IReadOnlyList<SelectListItem> Contacts { get; set; } = [];
    public IReadOnlyList<SelectListItem> Leads { get; set; } = [];
    public IReadOnlyList<SelectListItem> Opportunities { get; set; } = [];
}