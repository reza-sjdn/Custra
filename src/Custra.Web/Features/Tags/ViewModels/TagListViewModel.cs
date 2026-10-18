namespace Custra.Web.Features.Tags.ViewModels;


public class TagListViewModel
{
    public IReadOnlyList<TagViewModel> Items { get; set; }
        = Array.Empty<TagViewModel>();

    public string? Search { get; set; }

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public int TotalCount { get; set; }

    public int TotalPages =>
        (int)Math.Ceiling(TotalCount / (double)PageSize);
}
