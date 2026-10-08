using Custra.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Domain.Activities;

public sealed class Activity : OrganizationOwnedEntity
{
    public string Subject { get; private set; }
    public string? Description { get; private set; }

    public ActivityType Type { get; private set; }
    public ActivityStatus Status { get; private set; }

    public DateTime? DueDate { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    public Guid OwnerUserId { get; private set; }

    public Guid? CustomerId { get; private set; }
    public Guid? ContactId { get; private set; }
    public Guid? LeadId { get; private set; }
    public Guid? OpportunityId { get; private set; }

    private Activity()
    {
    }

    public Activity(
        string subject,
        ActivityType type,
        Guid ownerUserId,
        DateTime? dueDate = null,
        string? description = null,
        Guid? customerId = null,
        Guid? contactId = null,
        Guid? leadId = null,
        Guid? opportunityId = null)
    {
        if (string.IsNullOrWhiteSpace(subject))
            throw new ArgumentException(
                "Activity subject is required.",
                nameof(subject));

        if (ownerUserId == Guid.Empty)
            throw new ArgumentException(
                "Activity owner is required.",
                nameof(ownerUserId));

        Subject = subject.Trim();
        Description = description?.Trim();

        Type = type;
        Status = ActivityStatus.Planned;

        OwnerUserId = ownerUserId;
        DueDate = dueDate;

        CustomerId = customerId;
        ContactId = contactId;
        LeadId = leadId;
        OpportunityId = opportunityId;
    }

    public void Update(
        string subject,
        ActivityType type,
        Guid ownerUserId,
        DateTime? dueDate,
        string? description,
        Guid? customerId,
        Guid? contactId,
        Guid? leadId,
        Guid? opportunityId)
    {
        if (string.IsNullOrWhiteSpace(subject))
            throw new ArgumentException(
                "Activity subject is required.",
                nameof(subject));

        if (ownerUserId == Guid.Empty)
            throw new ArgumentException(
                "Activity owner is required.",
                nameof(ownerUserId));

        Subject = subject.Trim();
        Description = description?.Trim();

        Type = type;
        OwnerUserId = ownerUserId;
        DueDate = dueDate;

        CustomerId = customerId;
        ContactId = contactId;
        LeadId = leadId;
        OpportunityId = opportunityId;
    }

    public void Complete()
    {
        if (Status == ActivityStatus.Completed)
            return;

        Status = ActivityStatus.Completed;
        CompletedAt = DateTime.Now;
    }

    public void Cancel()
    {
        if (Status == ActivityStatus.Completed)
            throw new InvalidOperationException(
                "A completed activity cannot be cancelled.");

        Status = ActivityStatus.Cancelled;
    }

    public void Reopen()
    {
        Status = ActivityStatus.Planned;
        CompletedAt = null;
    }
}