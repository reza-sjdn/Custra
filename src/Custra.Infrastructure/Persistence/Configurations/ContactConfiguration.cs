using Custra.Domain.Contacts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Configurations;

public sealed class ContactConfiguration
    : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.JobTitle)
            .HasMaxLength(150);

        builder.Property(x => x.Email)
            .HasMaxLength(320);

        builder.Property(x => x.Phone)
            .HasMaxLength(50);

        builder.HasIndex(x => new
        {
            x.OrganizationId,
            x.CustomerId
        });

        builder.HasOne(x => x.Customer)
            .WithMany(x => x.Contacts)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}