namespace Custra.Infrastructure.Identity;

public sealed class OrganizationContext : IOrganizationContext
{
    public Guid OrganizationId { get; private set; } = Guid.Parse("74D286A7-FBAA-4BC6-F4FD-08DF1A8250A6"); // I've set a default organization to prevent the error by notification 

    public void SetOrganization(Guid organizationId)
    {
        if (organizationId == Guid.Empty)
            throw new ArgumentException(
                "Organization ID is required.",
                nameof(organizationId));

        OrganizationId = organizationId;
    }
}