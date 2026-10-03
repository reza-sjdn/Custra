using Custra.Domain.Leads;

namespace Custra.Web.Features.Leads.ViewModels;

public sealed class LeadViewModel
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? CompanyName { get; set; }
    public string? JobTitle { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }

    public LeadStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public string FullName =>
        $"{FirstName} {LastName}";
}