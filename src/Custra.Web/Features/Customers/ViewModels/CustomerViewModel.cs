namespace Custra.Web.Features.Customers.ViewModels;

public sealed record CustomerViewModel(
    Guid Id,
    string Name,
    DateTime CreatedAt);