namespace Custra.Web.Features.Contacts.ViewModels;

public sealed class ContactViewModel
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? JobTitle { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }

    public DateTime CreatedAt { get; set; }

    public string FullName =>
        $"{FirstName} {LastName}";
}