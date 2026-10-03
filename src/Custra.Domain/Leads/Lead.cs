using Custra.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Domain.Leads;

public sealed class Lead : OrganizationOwnedEntity
{
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string? CompanyName { get; private set; }
    public string? JobTitle { get; private set; }
    public string? Email { get; private set; }
    public string? Phone { get; private set; }

    public LeadStatus Status { get; private set; }

    private Lead()
    {
    }

    public Lead(
        string firstName,
        string lastName,
        string? companyName = null,
        string? jobTitle = null,
        string? email = null,
        string? phone = null)
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
        CompanyName = companyName?.Trim();
        JobTitle = jobTitle?.Trim();
        Email = email?.Trim();
        Phone = phone?.Trim();

        Status = LeadStatus.New;
    }

    public void Update(
        string firstName,
        string lastName,
        string? companyName,
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
        CompanyName = companyName?.Trim();
        JobTitle = jobTitle?.Trim();
        Email = email?.Trim();
        Phone = phone?.Trim();
    }

    public void ChangeStatus(LeadStatus status)
    {
        Status = status;
    }
}