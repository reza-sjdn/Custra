using Custra.Domain.Opportunities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Custra.Web.Features.Opportunities.ViewModels;

public class OpportunityListViewModel
{
    public IReadOnlyList<OpportunityViewModel> Opportunities { get; set; } = [];

    public string? Search { get; set; }
    public OpportunityStatus? Status { get; set; }

    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }

    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < TotalPages;

    public Guid? TagId { get; set; }
    public IReadOnlyList<SelectListItem> AvailableTags { get; set; }
        = Array.Empty<SelectListItem>();
}