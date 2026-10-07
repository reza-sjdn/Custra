using Custra.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Domain.Opportunities;

public sealed class SalesPipelineStage : OrganizationOwnedEntity
{
    public Guid SalesPipelineId { get; private set; }

    public string Name { get; private set; }

    public int Order { get; private set; }

    public decimal? Probability { get; private set; }

    public SalesPipeline SalesPipeline { get; private set; } = null!;

    public ICollection<Opportunity> Opportunities { get; private set; } = [];

    private SalesPipelineStage()
    {
    }

    public SalesPipelineStage(
        Guid salesPipelineId,
        string name,
        int order,
        decimal? probability = null)
    {
        if (salesPipelineId == Guid.Empty)
            throw new ArgumentException("Pipeline ID is required.", nameof(salesPipelineId));

        SalesPipelineId = salesPipelineId;

        SetName(name);
        SetOrder(order);
        SetProbability(probability);
    }

    public void Update(
        string name,
        int order,
        decimal? probability)
    {
        SetName(name);
        SetOrder(order);
        SetProbability(probability);
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Stage name is required.", nameof(name));

        Name = name.Trim();
    }

    private void SetOrder(int order)
    {
        if (order < 1)
            throw new ArgumentException("Stage order must be greater than zero.", nameof(order));

        Order = order;
    }

    private void SetProbability(decimal? probability)
    {
        if (probability is < 0 or > 100)
            throw new ArgumentException(
                "Probability must be between 0 and 100.",
                nameof(probability));

        Probability = probability;
    }
}