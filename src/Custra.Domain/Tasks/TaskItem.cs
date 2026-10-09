using Custra.Domain.Activities;
using Custra.Domain.Common;
using Custra.Domain.Contacts;
using Custra.Domain.Customers;
using Custra.Domain.Leads;
using Custra.Domain.Opportunities;

namespace Custra.Domain.Tasks;

public sealed class TaskItem : OrganizationOwnedEntity
{
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }

    public TaskItemStatus Status { get; private set; }
    public TaskItemPriority Priority { get; private set; }

    public DateTime? DueDate { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    public Guid OwnerUserId { get; private set; }

    public Guid? CustomerId { get; private set; }
    public Customer? Customer { get; private set; }

    public Guid? ContactId { get; private set; }
    public Contact? Contact { get; private set; }

    public Guid? LeadId { get; private set; }
    public Lead? Lead { get; private set; }

    public Guid? OpportunityId { get; private set; }
    public Opportunity? Opportunity { get; private set; }

    public Guid? ActivityId { get; private set; }
    public Activity? Activity { get; private set; }

    private TaskItem()
    {
    }

    public TaskItem(
        string title,
        Guid ownerUserId,
        TaskItemPriority priority = TaskItemPriority.Normal,
        string? description = null,
        DateTime? dueDate = null,
        Guid? customerId = null,
        Guid? contactId = null,
        Guid? leadId = null,
        Guid? opportunityId = null,
        Guid? activityId = null)
    {
        SetTitle(title);
        SetOwner(ownerUserId);
        SetPriority(priority);
        SetDescription(description);
        SetDueDate(dueDate);
        SetRelationships(
            customerId,
            contactId,
            leadId,
            opportunityId,
            activityId);

        Status = TaskItemStatus.NotStarted;
    }

    public void Update(
        string title,
        Guid ownerUserId,
        TaskItemPriority priority,
        string? description,
        DateTime? dueDate,
        Guid? customerId,
        Guid? contactId,
        Guid? leadId,
        Guid? opportunityId,
        Guid? activityId)
    {
        SetTitle(title);
        SetOwner(ownerUserId);
        SetPriority(priority);
        SetDescription(description);
        SetDueDate(dueDate);
        SetRelationships(
            customerId,
            contactId,
            leadId,
            opportunityId,
            activityId);
    }

    public void Start()
    {
        if (Status != TaskItemStatus.NotStarted)
        {
            throw new InvalidOperationException(
                "Only a task that has not started can be started.");
        }

        Status = TaskItemStatus.InProgress;
    }

    public void Complete()
    {
        if (Status == TaskItemStatus.Cancelled)
        {
            throw new InvalidOperationException(
                "A cancelled task cannot be completed.");
        }

        if (Status == TaskItemStatus.Completed)
        {
            return;
        }

        Status = TaskItemStatus.Completed;
        CompletedAt = DateTime.Now;
    }

    public void Cancel()
    {
        if (Status == TaskItemStatus.Completed)
        {
            throw new InvalidOperationException(
                "A completed task cannot be cancelled.");
        }

        Status = TaskItemStatus.Cancelled;
    }

    public void Reopen()
    {
        if (Status is not TaskItemStatus.Completed
            and not TaskItemStatus.Cancelled)
        {
            throw new InvalidOperationException(
                "Only a completed or cancelled task can be reopened.");
        }

        Status = TaskItemStatus.NotStarted;
        CompletedAt = null;
    }

    private void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException(
                "Task title is required.",
                nameof(title));
        }

        Title = title.Trim();
    }

    private void SetOwner(Guid ownerUserId)
    {
        if (ownerUserId == Guid.Empty)
        {
            throw new ArgumentException(
                "Task owner is required.",
                nameof(ownerUserId));
        }

        OwnerUserId = ownerUserId;
    }

    private void SetPriority(TaskItemPriority priority)
    {
        if (!Enum.IsDefined(priority))
        {
            throw new ArgumentOutOfRangeException(nameof(priority));
        }

        Priority = priority;
    }

    private void SetDescription(string? description)
    {
        Description = string.IsNullOrWhiteSpace(description)
            ? null
            : description.Trim();
    }

    private void SetDueDate(DateTime? dueDate)
    {
        DueDate = dueDate;
    }

    private void SetRelationships(
        Guid? customerId,
        Guid? contactId,
        Guid? leadId,
        Guid? opportunityId,
        Guid? activityId)
    {
        ValidateOptionalId(customerId, nameof(customerId));
        ValidateOptionalId(contactId, nameof(contactId));
        ValidateOptionalId(leadId, nameof(leadId));
        ValidateOptionalId(opportunityId, nameof(opportunityId));
        ValidateOptionalId(activityId, nameof(activityId));

        CustomerId = customerId;
        ContactId = contactId;
        LeadId = leadId;
        OpportunityId = opportunityId;
        ActivityId = activityId;
    }

    private static void ValidateOptionalId(Guid? id, string parameterName)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException(
                "An optional relationship ID cannot be an empty GUID.",
                parameterName);
        }
    }
}