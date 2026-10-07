using Custra.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Domain.Opportunities;

public sealed class SalesPipeline : OrganizationOwnedEntity
{
    public string Name { get; private set; }

    public ICollection<SalesPipelineStage> Stages { get; private set; } = [];

    private SalesPipeline()
    {
    }

    public SalesPipeline(string name)
    {
        SetName(name);
    }

    public void UpdateName(string name)
    {
        SetName(name);
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Pipeline name is required.", nameof(name));

        Name = name.Trim();
    }
}