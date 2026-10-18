using Custra.Application.Tags.DTOs;

namespace Custra.Web.Features.Tags.ViewModels;


public class TagViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = "#808080";
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public int CustomerCount { get; init; }
    public int ContactCount { get; init; }
    public int LeadCount { get; init; }
    public int OpportunityCount { get; init; }
    public int TotalUsageCount { get; init; }
    public IReadOnlyList<TagUsageDto> Usages { get; set; } = [];
}
