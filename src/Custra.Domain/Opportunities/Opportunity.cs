using Custra.Domain.Common;
using Custra.Domain.Contacts;
using Custra.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Domain.Opportunities;

public sealed class Opportunity : OrganizationOwnedEntity
{
    public Guid CustomerId { get; private set; }

    public Guid? ContactId { get; private set; }

    public Guid OwnerUserId { get; private set; }

    public Guid SalesPipelineId { get; private set; }

    public Guid SalesPipelineStageId { get; private set; }

    public string Title { get; private set; }

    public string? Description { get; private set; }

    public decimal? EstimatedValue { get; private set; }

    public DateTime? ExpectedCloseDate { get; private set; }

    public OpportunityStatus Status { get; private set; }

    public Customer Customer { get; private set; } = null!;

    public Contact? Contact { get; private set; }

    public SalesPipeline SalesPipeline { get; private set; } = null!;

    public SalesPipelineStage SalesPipelineStage { get; private set; } = null!;

    private Opportunity()
    {
    }

    public Opportunity(
        Guid customerId,
        Guid? contactId,
        Guid ownerUserId,
        Guid salesPipelineId,
        Guid salesPipelineStageId,
        string title,
        string? description,
        decimal? estimatedValue,
        DateTime? expectedCloseDate)
    {
        if (customerId == Guid.Empty)
            throw new ArgumentException("Customer ID is required.", nameof(customerId));

        if (ownerUserId == Guid.Empty)
            throw new ArgumentException("Owner user ID is required.", nameof(ownerUserId));

        if (salesPipelineId == Guid.Empty)
            throw new ArgumentException(
                "Sales pipeline ID is required.",
                nameof(salesPipelineId));

        if (salesPipelineStageId == Guid.Empty)
            throw new ArgumentException(
                "Sales pipeline stage ID is required.",
                nameof(salesPipelineStageId));

        CustomerId = customerId;
        ContactId = contactId;
        OwnerUserId = ownerUserId;
        SalesPipelineId = salesPipelineId;
        SalesPipelineStageId = salesPipelineStageId;

        SetTitle(title);
        SetDescription(description);
        SetEstimatedValue(estimatedValue);
        ExpectedCloseDate = expectedCloseDate;

        Status = OpportunityStatus.Open;
    }

    public void Update(
        Guid customerId,
        Guid? contactId,
        Guid ownerUserId,
        Guid salesPipelineId,
        string title,
        string? description,
        decimal? estimatedValue,
        DateTime? expectedCloseDate)
    {
        if (customerId == Guid.Empty)
            throw new ArgumentException("Customer ID is required.", nameof(customerId));

        if (ownerUserId == Guid.Empty)
            throw new ArgumentException("Owner user ID is required.", nameof(ownerUserId));

        if (salesPipelineId == Guid.Empty)
            throw new ArgumentException("Sales Pipeline ID is required.", nameof(salesPipelineId));

        CustomerId = customerId;
        ContactId = contactId;
        OwnerUserId = ownerUserId;
        SalesPipelineId = salesPipelineId;

        SetTitle(title);
        SetDescription(description);
        SetEstimatedValue(estimatedValue);

        ExpectedCloseDate = expectedCloseDate;
    }

    public void ChangeStage(Guid salesPipelineStageId)
    {
        if (salesPipelineStageId == Guid.Empty)
            throw new ArgumentException(
                "Sales pipeline stage ID is required.",
                nameof(salesPipelineStageId));

        SalesPipelineStageId = salesPipelineStageId;
    }

    public void MarkAsWon()
    {
        Status = OpportunityStatus.Won;
    }

    public void MarkAsLost()
    {
        Status = OpportunityStatus.Lost;
    }

    public void Reopen()
    {
        Status = OpportunityStatus.Open;
    }

    private void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Opportunity title is required.", nameof(title));

        Title = title.Trim();
    }

    private void SetDescription(string? description)
    {
        Description = string.IsNullOrWhiteSpace(description)
            ? null
            : description.Trim();
    }

    private void SetEstimatedValue(decimal? estimatedValue)
    {
        if (estimatedValue is < 0)
            throw new ArgumentException(
                "Estimated value cannot be negative.",
                nameof(estimatedValue));

        EstimatedValue = estimatedValue;
    }
}