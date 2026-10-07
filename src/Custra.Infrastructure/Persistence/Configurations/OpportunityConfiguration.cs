using Custra.Domain.Opportunities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Configurations;

public sealed class OpportunityConfiguration
    : IEntityTypeConfiguration<Opportunity>
{
    public void Configure(EntityTypeBuilder<Opportunity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .HasMaxLength(2000);

        builder.Property(x => x.EstimatedValue)
            .HasPrecision(18, 2);

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
            x.CustomerId
        });

        builder.HasIndex(x => new
        {
            x.OrganizationId,
            x.OwnerUserId
        });

        builder.HasIndex(x => new
        {
            x.OrganizationId,
            x.SalesPipelineId,
            x.SalesPipelineStageId
        });

        builder.HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Contact)
            .WithMany()
            .HasForeignKey(x => x.ContactId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.SalesPipeline)
            .WithMany()
            .HasForeignKey(x => x.SalesPipelineId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.SalesPipelineStage)
            .WithMany(x => x.Opportunities)
            .HasForeignKey(x => x.SalesPipelineStageId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}