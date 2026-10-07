using Custra.Domain.Opportunities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Configurations;

public sealed class SalesPipelineConfiguration : IEntityTypeConfiguration<SalesPipeline>
{
    public void Configure(EntityTypeBuilder<SalesPipeline> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(x => new
        {
            x.OrganizationId,
            x.Name
        })
        .IsUnique();

        builder.HasMany(x => x.Stages)
            .WithOne(x => x.SalesPipeline)
            .HasForeignKey(x => x.SalesPipelineId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}