namespace Custra.Domain.Common;

public abstract class OrganizationOwnedEntity : AuditableEntity
{
    public Guid OrganizationId { get; protected set; }
}