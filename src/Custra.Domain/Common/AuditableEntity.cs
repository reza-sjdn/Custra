namespace Custra.Domain.Common;

public abstract class AuditableEntity : Entity
{
    public DateTime CreatedAt { get; protected set; }
    public DateTime? ModifiedAt { get; protected set; }
    public Guid? CreatedBy { get; protected set; }
    public Guid? ModifiedBy { get; protected set; }
}