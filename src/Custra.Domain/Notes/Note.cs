using Custra.Domain.Common;
using Custra.Domain.Contacts;
using Custra.Domain.Customers;
using Custra.Domain.Leads;
using Custra.Domain.Opportunities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Domain.Notes;


public sealed class Note : OrganizationOwnedEntity
{
    public string Title { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;

    public Guid? CustomerId { get; private set; }
    public Customer? Customer { get; private set; }

    public Guid? ContactId { get; private set; }
    public Contact? Contact { get; private set; }

    public Guid? LeadId { get; private set; }
    public Lead? Lead { get; private set; }

    public Guid? OpportunityId { get; private set; }
    public Opportunity? Opportunity { get; private set; }

    private Note() { }

    public Note(
        string title,
        string content,
        Guid? customerId,
        Guid? contactId,
        Guid? leadId,
        Guid? opportunityId)
    {
        SetDetails(title, content);
        SetRelatedRecords(customerId, contactId, leadId, opportunityId);
    }

    public void Update(
        string title,
        string content,
        Guid? customerId,
        Guid? contactId,
        Guid? leadId,
        Guid? opportunityId)
    {
        SetDetails(title, content);
        SetRelatedRecords(customerId, contactId, leadId, opportunityId);
    }

    private void SetDetails(string title, string content)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Note title is required.", nameof(title));

        if (title.Trim().Length > 200)
            throw new ArgumentException("Note title cannot exceed 200 characters.", nameof(title));

        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("Note content is required.", nameof(content));

        if (content.Length > 10000)
            throw new ArgumentException("Note content cannot exceed 10,000 characters.", nameof(content));

        Title = title.Trim();
        Content = content.Trim();
    }

    private void SetRelatedRecords(
        Guid? customerId,
        Guid? contactId,
        Guid? leadId,
        Guid? opportunityId)
    {
        if (customerId == Guid.Empty ||
            contactId == Guid.Empty ||
            leadId == Guid.Empty ||
            opportunityId == Guid.Empty)
        {
            throw new ArgumentException("Related record IDs cannot be empty GUIDs.");
        }

        if (!customerId.HasValue &&
            !contactId.HasValue &&
            !leadId.HasValue &&
            !opportunityId.HasValue)
        {
            throw new ArgumentException(
                "A note must be associated with at least one CRM record.");
        }

        CustomerId = customerId;
        ContactId = contactId;
        LeadId = leadId;
        OpportunityId = opportunityId;
    }
}