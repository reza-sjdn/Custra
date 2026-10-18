using Custra.Web.Features.Tags.ViewModels;

namespace Custra.Web.Features.Customers.ViewModels;

public sealed class CustomerViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public TagAssignmentViewModel TagAssignments { get; set; } = new();
}