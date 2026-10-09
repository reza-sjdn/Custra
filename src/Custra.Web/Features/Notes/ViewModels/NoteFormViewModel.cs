using Microsoft.AspNetCore.Mvc.Rendering;

namespace Custra.Web.Features.Notes.ViewModels;


public sealed class NoteFormViewModel
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;

    public Guid? CustomerId { get; set; }
    public Guid? ContactId { get; set; }
    public Guid? LeadId { get; set; }
    public Guid? OpportunityId { get; set; }

    public IReadOnlyList<SelectListItem> Customers { get; set; }
        = Array.Empty<SelectListItem>();

    public IReadOnlyList<SelectListItem> Contacts { get; set; }
        = Array.Empty<SelectListItem>();

    public IReadOnlyList<SelectListItem> Leads { get; set; }
        = Array.Empty<SelectListItem>();

    public IReadOnlyList<SelectListItem> Opportunities { get; set; }
        = Array.Empty<SelectListItem>();
}