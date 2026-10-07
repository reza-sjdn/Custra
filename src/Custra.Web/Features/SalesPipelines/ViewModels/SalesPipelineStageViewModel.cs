namespace Custra.Web.Features.SalesPipelines.ViewModels;

public class SalesPipelineStageViewModel
{
    public Guid Id { get; set; }
    public Guid SalesPipelineId { get; set; }

    public string Name { get; set; } = string.Empty;
    public int Order { get; set; }
    public decimal? Probability { get; set; }
}