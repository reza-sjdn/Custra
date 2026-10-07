using Custra.Domain.Opportunities;

namespace Custra.Web.Features.Opportunities.ViewModels;

public class OpportunityViewModel
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid? ContactId { get; set; }
    public Guid OwnerUserId { get; set; }
    public Guid SalesPipelineId { get; set; }
    public Guid SalesPipelineStageId { get; set; }

    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal? EstimatedValue { get; set; }
    public DateTime? ExpectedCloseDate { get; set; }
    public OpportunityStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }
}