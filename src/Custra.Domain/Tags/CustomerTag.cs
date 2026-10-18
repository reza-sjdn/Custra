using Custra.Domain.Common;
using Custra.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Domain.Tags;


public class CustomerTag : OrganizationOwnedEntity
{
    public Guid CustomerId { get; private set; }
    public Customer Customer { get; private set; } = null!;

    public Guid TagId { get; private set; }
    public Tag Tag { get; private set; } = null!;

    private CustomerTag()
    {
    }

    public CustomerTag(Guid customerId, Guid tagId)
    {
        if (customerId == Guid.Empty)
            throw new ArgumentException("Customer ID is required.", nameof(customerId));

        if (tagId == Guid.Empty)
            throw new ArgumentException("Tag ID is required.", nameof(tagId));

        CustomerId = customerId;
        TagId = tagId;
    }
}

