using Custra.Domain.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Configurations;

public class OpportunityTagConfiguration : IEntityTypeConfiguration<OpportunityTag>
{
    public void Configure(EntityTypeBuilder<OpportunityTag> builder)
    {
        builder.ToTable("OpportunityTags");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new
        {
            x.OrganizationId,
            x.OpportunityId,
            x.TagId
        }).IsUnique();

        builder.HasOne(x => x.Opportunity)
            .WithMany()
            .HasForeignKey(x => x.OpportunityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Tag)
            .WithMany(x => x.OpportunityTags)
            .HasForeignKey(x => x.TagId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}