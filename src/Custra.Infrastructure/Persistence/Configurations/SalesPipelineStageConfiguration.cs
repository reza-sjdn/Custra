using Custra.Domain.Opportunities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Configurations;

public sealed class SalesPipelineStageConfiguration
    : IEntityTypeConfiguration<SalesPipelineStage>
{
    public void Configure(EntityTypeBuilder<SalesPipelineStage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Order)
            .IsRequired();

        builder.Property(x => x.Probability)
            .HasPrecision(5, 2);

        builder.HasIndex(x => new
        {
            x.OrganizationId,
            x.SalesPipelineId,
            x.Order
        })
        .IsUnique();

        builder.HasIndex(x => new
        {
            x.OrganizationId,
            x.SalesPipelineId,
            x.Name
        })
        .IsUnique();

        builder.HasMany(x => x.Opportunities)
            .WithOne(x => x.SalesPipelineStage)
            .HasForeignKey(x => x.SalesPipelineStageId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}