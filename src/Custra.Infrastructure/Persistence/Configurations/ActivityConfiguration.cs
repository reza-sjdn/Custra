using Custra.Domain.Activities;
using Custra.Domain.Contacts;
using Custra.Domain.Customers;
using Custra.Domain.Leads;
using Custra.Domain.Opportunities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Configurations;

public sealed class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.ToTable("Activities");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Subject)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .HasMaxLength(2000);

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.OwnerUserId)
            .IsRequired();

        builder.HasIndex(x => new
        {
            x.OrganizationId,
            x.Status
        });

        builder.HasIndex(x => new
        {
            x.OrganizationId,
            x.OwnerUserId
        });

        builder.HasIndex(x => new
        {
            x.OrganizationId,
            x.DueDate
        });

        builder.HasIndex(x => new
        {
            x.OrganizationId,
            x.CustomerId
        });

        builder.HasIndex(x => new
        {
            x.OrganizationId,
            x.ContactId
        });

        builder.HasIndex(x => new
        {
            x.OrganizationId,
            x.LeadId
        });

        builder.HasIndex(x => new
        {
            x.OrganizationId,
            x.OpportunityId
        });

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Contact>()
            .WithMany()
            .HasForeignKey(x => x.ContactId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Lead>()
            .WithMany()
            .HasForeignKey(x => x.LeadId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Opportunity>()
            .WithMany()
            .HasForeignKey(x => x.OpportunityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}