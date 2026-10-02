using Custra.Domain.Common;
using Custra.Domain.Contacts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Domain.Customers;

public sealed class Customer : OrganizationOwnedEntity
{
    public string Name { get; private set; } = null!;

    public ICollection<Contact> Contacts { get; private set; } = [];

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