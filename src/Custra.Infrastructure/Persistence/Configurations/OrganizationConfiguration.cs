using Custra.Domain.Organizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Custra.Infrastructure.Persistence.Configurations;

public sealed class OrganizationConfiguration
    : IEntityTypeConfiguration<Organization>
{
    public void Configure(
        EntityTypeBuilder<Organization> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(x => x.Name);
    }
}