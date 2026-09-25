using Custra.Domain.Common;

namespace Custra.Domain.Organizations;

public sealed class Organization : AuditableEntity
{
    public string Name { get; private set; } = null!;

    private Organization()
    {
    }

    public Organization(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Organization name is required.",
                nameof(name));

        Name = name.Trim();
        CreatedAt = DateTime.UtcNow;
    }
}