using Custra.Domain.Organizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Custra.Infrastructure.Persistence.Configurations;

public sealed class OrganizationMembershipConfiguration
    : IEntityTypeConfiguration<OrganizationMembership>
{
    public void Configure(EntityTypeBuilder<OrganizationMembership> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.OrganizationId)
            .IsRequired();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.RoleId)
            .IsRequired();

        builder.HasIndex(x => new
        {
            x.OrganizationId,
            x.UserId
        })
        .IsUnique();

        builder.HasOne(x => x.Organization)
            .WithMany()
            .HasForeignKey(x => x.OrganizationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}