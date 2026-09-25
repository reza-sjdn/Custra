using Custra.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Domain.Customers;

public sealed class Customer : OrganizationOwnedEntity
{
    public string Name { get; private set; } = null!;

    private Customer()
    {
    }

    public Customer(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Customer name is required.",
                nameof(name));

        Name = name.Trim();
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Customer name is required.",
                nameof(name));

        Name = name.Trim();
    }
}