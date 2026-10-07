namespace Custra.Web.Features.SalesPipelines.ViewModels;

public class SalesPipelineViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public IReadOnlyList<SalesPipelineStageViewModel> Stages { get; set; } = [];
}