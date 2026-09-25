namespace Custra.Web.Features.Customers.ViewModels;

public sealed class CustomerListViewModel
{
    public IReadOnlyList<CustomerViewModel> Customers { get; init; }
        = [];

    public string? Search { get; init; }

    public int Page { get; init; }

    public int PageSize { get; init; }

    public int TotalCount { get; init; }

    public int TotalPages { get; init; }

    public bool HasPreviousPage => Page > 1;

    public bool HasNextPage => Page < TotalPages;
}