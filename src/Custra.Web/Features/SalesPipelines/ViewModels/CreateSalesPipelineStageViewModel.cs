namespace Custra.Web.Features.SalesPipelines.ViewModels;

public class CreateSalesPipelineStageViewModel
{
    public Guid SalesPipelineId { get; set; }

    public string Name { get; set; } = string.Empty;
    public int Order { get; set; }
    public decimal? Probability { get; set; }
}