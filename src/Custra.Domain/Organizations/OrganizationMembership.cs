using Custra.Domain.Authorization;
using Custra.Domain.Common;

namespace Custra.Domain.Organizations;

public sealed class OrganizationMembership : Entity
{
    public Guid OrganizationId { get; private set; }
    public Guid UserId { get; private set; }
    public Guid RoleId { get; private set; }

    public Organization Organization { get; private set; } = null!;
    public Role? Role { get; private set; }


    private OrganizationMembership()
    {
    }

    public OrganizationMembership(
        Guid organizationId,
        Guid userId,
        Guid roleId)
    {
        if (organizationId == Guid.Empty)
            throw new ArgumentException(
                "Organization ID is required.",
                nameof(organizationId));

        if (userId == Guid.Empty)
            throw new ArgumentException(
                "User ID is required.",
                nameof(userId));

        if (roleId == Guid.Empty)
            throw new ArgumentException(
                "Role ID is required.",
                nameof(roleId));

        OrganizationId = organizationId;
        UserId = userId;
        RoleId = roleId;
    }
}