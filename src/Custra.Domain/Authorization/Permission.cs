using Custra.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Domain.Authorization;

public sealed class Permission : Entity
{
    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }

    private Permission()
    {
    }

    public Permission(string name, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Permission name is required.",
                nameof(name));

        Name = name.Trim();
        Description = description?.Trim();
    }
}