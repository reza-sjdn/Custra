using Custra.Domain.Leads;

namespace Custra.Web.Features.Leads.ViewModels;

public sealed class LeadListViewModel
{
    public IReadOnlyList<LeadViewModel> Leads { get; init; } = [];

    public string? Search { get; init; }
    public LeadStatus? Status { get; init; }

    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
    public int TotalPages { get; init; }

    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < TotalPages;
}