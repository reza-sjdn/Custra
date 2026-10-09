namespace Custra.Web.Features.Notes.ViewModels;


public sealed class NoteListViewModel
{
    public IReadOnlyList<NoteViewModel> Items { get; set; }
        = Array.Empty<NoteViewModel>();

    public string? Search { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? ContactId { get; set; }
    public Guid? LeadId { get; set; }
    public Guid? OpportunityId { get; set; }

    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int TotalCount { get; set; }

    public int TotalPages =>
        (int)Math.Ceiling(TotalCount / (double)PageSize);
}