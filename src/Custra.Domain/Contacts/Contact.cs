using Custra.Domain.Common;
using Custra.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Domain.Contacts;

public sealed class Contact : OrganizationOwnedEntity
{
    public Guid CustomerId { get; private set; }

    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string? JobTitle { get; private set; }
    public string? Email { get; private set; }
    public string? Phone { get; private set; }

    public Customer Customer { get; private set; } = null!;

    private Contact()
    {
    }

    public Contact(
        Guid customerId,
        string firstName,
        string lastName,
        string? jobTitle = null,
        string? email = null,
        string? phone = null)
    {
        if (customerId == Guid.Empty)
            throw new ArgumentException(
                "Customer ID is required.",
                nameof(customerId));

        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException(
                "First name is required.",
                nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException(
                "Last name is required.",
                nameof(lastName));

        CustomerId = customerId;
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        JobTitle = jobTitle?.Trim();
        Email = email?.Trim();
        Phone = phone?.Trim();
    }

    public void Update(
        string firstName,
        string lastName,
        string? jobTitle,
        string? email,
        string? phone)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException(
                "First name is required.",
                nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException(
                "Last name is required.",
                nameof(lastName));

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        JobTitle = jobTitle?.Trim();
        Email = email?.Trim();
        Phone = phone?.Trim();
    }

}