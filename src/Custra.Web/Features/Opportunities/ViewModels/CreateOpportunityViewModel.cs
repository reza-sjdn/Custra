using Custra.Web.Common.ViewModels;

namespace Custra.Web.Features.Opportunities.ViewModels;

public class CreateOpportunityViewModel
{
    public Guid CustomerId { get; set; }
    public Guid? ContactId { get; set; }
    public Guid OwnerUserId { get; set; }
    public Guid SalesPipelineId { get; set; }
    public Guid SalesPipelineStageId { get; set; }

    public IReadOnlyList<LookupItemViewModel> Customers { get; set; }
        = [];
    public IReadOnlyList<LookupItemViewModel> Contacts { get; set; }
        = [];
    public IReadOnlyList<LookupItemViewModel> Owners { get; set; }
        = [];
    public IReadOnlyList<LookupItemViewModel> Pipelines { get; set; }
        = [];
    public IReadOnlyList<LookupItemViewModel> Stages { get; set; }
        = [];

    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal? EstimatedValue { get; set; }
    public DateTime? ExpectedCloseDate { get; set; }
}