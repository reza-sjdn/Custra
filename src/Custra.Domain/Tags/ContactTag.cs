using Custra.Domain.Common;
using Custra.Domain.Contacts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Domain.Tags;

public class ContactTag : OrganizationOwnedEntity
{
    public Guid ContactId { get; private set; }
    public Contact Contact { get; private set; } = null!;

    public Guid TagId { get; private set; }
    public Tag Tag { get; private set; } = null!;

    private ContactTag()
    {
    }

    public ContactTag(Guid contactId, Guid tagId)
    {
        if (contactId == Guid.Empty)
            throw new ArgumentException("Contact ID is required.", nameof(contactId));

        if (tagId == Guid.Empty)
            throw new ArgumentException("Tag ID is required.", nameof(tagId));

        ContactId = contactId;
        TagId = tagId;
    }
}