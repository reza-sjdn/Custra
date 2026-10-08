using Custra.Domain.Activities;
using Custra.Web.Common.ViewModels;

namespace Custra.Web.Features.Activities.ViewModels;

public sealed class CreateActivityViewModel
{
    public string Subject { get; set; } = string.Empty;
    public string? Description { get; set; }

    public ActivityType Type { get; set; }

    public Guid OwnerUserId { get; set; }

    public DateTime? DueDate { get; set; }

    public Guid? CustomerId { get; set; }
    public Guid? ContactId { get; set; }
    public Guid? LeadId { get; set; }
    public Guid? OpportunityId { get; set; }

    public IReadOnlyList<LookupItemViewModel> Owners { get; set; } = [];
    public IReadOnlyList<LookupItemViewModel> Customers { get; set; } = [];
    public IReadOnlyList<LookupItemViewModel> Contacts { get; set; } = [];
    public IReadOnlyList<LookupItemViewModel> Leads { get; set; } = [];
    public IReadOnlyList<LookupItemViewModel> Opportunities { get; set; } = [];
}