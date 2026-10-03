using Custra.Domain.Leads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Configurations;

public sealed class LeadConfiguration
    : IEntityTypeConfiguration<Lead>
{
    public void Configure(EntityTypeBuilder<Lead> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.CompanyName)
            .HasMaxLength(200);

        builder.Property(x => x.JobTitle)
            .HasMaxLength(150);

        builder.Property(x => x.Email)
            .HasMaxLength(320);

        builder.Property(x => x.Phone)
            .HasMaxLength(50);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.HasIndex(x => new
        {
            x.OrganizationId,
            x.Status
        });

        builder.HasIndex(x => new
        {
            x.OrganizationId,
            x.LastName,
            x.FirstName
        });
    }
}