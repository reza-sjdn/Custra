using Custra.Domain.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Configurations;

public class LeadTagConfiguration : IEntityTypeConfiguration<LeadTag>
{
    public void Configure(EntityTypeBuilder<LeadTag> builder)
    {
        builder.ToTable("LeadTags");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new
        {
            x.OrganizationId,
            x.LeadId,
            x.TagId
        }).IsUnique();

        builder.HasOne(x => x.Lead)
            .WithMany()
            .HasForeignKey(x => x.LeadId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Tag)
            .WithMany(x => x.LeadTags)
            .HasForeignKey(x => x.TagId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}