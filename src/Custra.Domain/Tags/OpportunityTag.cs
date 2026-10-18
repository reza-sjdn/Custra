using Custra.Domain.Common;
using Custra.Domain.Opportunities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Domain.Tags;

public class OpportunityTag : OrganizationOwnedEntity
{
    public Guid OpportunityId { get; private set; }
    public Opportunity Opportunity { get; private set; } = null!;

    public Guid TagId { get; private set; }
    public Tag Tag { get; private set; } = null!;

    private OpportunityTag()
    {
    }

    public OpportunityTag(Guid opportunityId, Guid tagId)
    {
        if (opportunityId == Guid.Empty)
            throw new ArgumentException("Opportunity ID is required.", nameof(opportunityId));

        if (tagId == Guid.Empty)
            throw new ArgumentException("Tag ID is required.", nameof(tagId));

        OpportunityId = opportunityId;
        TagId = tagId;
    }
}
