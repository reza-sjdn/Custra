namespace Custra.Web.Features.Contacts.ViewModels;

public sealed class ContactListViewModel
{
    public Guid CustomerId { get; init; }

    public IReadOnlyList<ContactViewModel> Contacts { get; init; } = [];

    public string? Search { get; init; }

    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
    public int TotalPages { get; init; }

    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < TotalPages;
}