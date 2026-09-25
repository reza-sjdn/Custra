using Custra.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Domain.Authorization;

public sealed class RolePermission : OrganizationOwnedEntity
{
    public Guid RoleId { get; private set; }
    public Guid PermissionId { get; private set; }

    public Role? Role { get; private set; }
    public Permission Permission { get; private set; } = null!;

    private RolePermission()
    {
    }

    public RolePermission(Guid roleId, Guid permissionId)
    {
        if (roleId == Guid.Empty)
            throw new ArgumentException(
                "Role ID is required.",
                nameof(roleId));

        if (permissionId == Guid.Empty)
            throw new ArgumentException(
                "Permission ID is required.",
                nameof(permissionId));

        RoleId = roleId;
        PermissionId = permissionId;
    }
}
