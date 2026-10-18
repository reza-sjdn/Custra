using Custra.Domain.Common;
using Custra.Domain.Leads;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Domain.Tags;

public class LeadTag : OrganizationOwnedEntity
{
    public Guid LeadId { get; private set; }
    public Lead Lead { get; private set; } = null!;

    public Guid TagId { get; private set; }
    public Tag Tag { get; private set; } = null!;

    private LeadTag()
    {
    }

    public LeadTag(Guid leadId, Guid tagId)
    {
        if (leadId == Guid.Empty)
            throw new ArgumentException("Lead ID is required.", nameof(leadId));

        if (tagId == Guid.Empty)
            throw new ArgumentException("Tag ID is required.", nameof(tagId));

        LeadId = leadId;
        TagId = tagId;
    }
}
