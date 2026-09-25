using Custra.Domain.Common;
using Custra.Domain.Organizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Domain.Authorization;

public sealed class Role : OrganizationOwnedEntity
{
    public string Name { get; private set; } = null!;

    public Organization Organization { get; private set; } = null!;

    private readonly List<RolePermission> _rolePermissions = [];
    public IReadOnlyCollection<RolePermission> RolePermissions =>
        _rolePermissions.AsReadOnly();

    private Role()
    {
    }

    public Role(Guid organizationId, string name)
    {
        if (organizationId == Guid.Empty)
            throw new ArgumentException(
                "Organization ID is required.",
                nameof(organizationId));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Role name is required.",
                nameof(name));

        OrganizationId = organizationId;
        Name = name.Trim();
    }
}