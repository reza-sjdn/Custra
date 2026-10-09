namespace Custra.Web.Features.Notes.ViewModels;


public sealed class NoteViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;

    public Guid? CustomerId { get; set; }
    public string? CustomerName { get; set; }

    public Guid? ContactId { get; set; }
    public string? ContactName { get; set; }

    public Guid? LeadId { get; set; }
    public string? LeadName { get; set; }

    public Guid? OpportunityId { get; set; }
    public string? OpportunityTitle { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}