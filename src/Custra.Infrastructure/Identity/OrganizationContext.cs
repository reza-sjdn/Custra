namespace Custra.Infrastructure.Identity;

public sealed class OrganizationContext : IOrganizationContext
{
    public Guid OrganizationId { get; private set; }

    public void SetOrganization(Guid organizationId)
    {
        if (organizationId == Guid.Empty)
            throw new ArgumentException(
                "Organization ID is required.",
                nameof(organizationId));

        OrganizationId = organizationId;
    }
}