using Custra.Domain.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Custra.Infrastructure.Persistence.Configurations;

public sealed class TaskItemConfiguration
    : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.ToTable("Tasks");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .HasMaxLength(2000);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.Priority)
            .IsRequired();

        builder.Property(x => x.OwnerUserId)
            .IsRequired();

        builder.HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Contact)
            .WithMany()
            .HasForeignKey(x => x.ContactId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Lead)
            .WithMany()
            .HasForeignKey(x => x.LeadId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Opportunity)
            .WithMany()
            .HasForeignKey(x => x.OpportunityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Activity)
            .WithMany()
            .HasForeignKey(x => x.ActivityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.OrganizationId, x.Status });
        builder.HasIndex(x => new { x.OrganizationId, x.DueDate });
        builder.HasIndex(x => new { x.OrganizationId, x.OwnerUserId });
        builder.HasIndex(x => x.CustomerId);
        builder.HasIndex(x => x.ContactId);
        builder.HasIndex(x => x.LeadId);
        builder.HasIndex(x => x.OpportunityId);
        builder.HasIndex(x => x.ActivityId);
    }
}