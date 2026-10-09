using Custra.Domain.Tasks;

namespace Custra.Web.Features.Tasks.ViewModels;

public sealed class TaskItemListViewModel
{
    public IReadOnlyList<TaskItemViewModel> Items { get; init; } = [];

    public string? Search { get; init; }
    public TaskItemStatus? Status { get; init; }
    public TaskItemPriority? Priority { get; init; }
    public Guid? OwnerUserId { get; init; }

    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public int TotalCount { get; init; }

    public int TotalPages =>
        (int)Math.Ceiling(TotalCount / (double)PageSize);
}