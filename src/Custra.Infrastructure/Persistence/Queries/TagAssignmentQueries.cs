using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Tags.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Queries;


public class TagAssignmentQueries : ITagAssignmentQueries
{
    private readonly CustraDbContext _dbContext;

    public TagAssignmentQueries(CustraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<TagAssignmentDto>> GetAssignedTagsAsync(
        string recordType,
        Guid recordId,
        CancellationToken cancellationToken = default)
    {
        return recordType switch
        {
            "Customer" => await _dbContext.CustomerTags
                .AsNoTracking()
                .Where(x => x.CustomerId == recordId)
                .OrderBy(x => x.Tag.Name)
                .Select(x => new TagAssignmentDto(
                    x.TagId, x.Tag.Name, x.Tag.Color))
                .ToListAsync(cancellationToken),

            "Contact" => await _dbContext.ContactTags
                .AsNoTracking()
                .Where(x => x.ContactId == recordId)
                .OrderBy(x => x.Tag.Name)
                .Select(x => new TagAssignmentDto(
                    x.TagId, x.Tag.Name, x.Tag.Color))
                .ToListAsync(cancellationToken),

            "Lead" => await _dbContext.LeadTags
                .AsNoTracking()
                .Where(x => x.LeadId == recordId)
                .OrderBy(x => x.Tag.Name)
                .Select(x => new TagAssignmentDto(
                    x.TagId, x.Tag.Name, x.Tag.Color))
                .ToListAsync(cancellationToken),

            "Opportunity" => await _dbContext.OpportunityTags
                .AsNoTracking()
                .Where(x => x.OpportunityId == recordId)
                .OrderBy(x => x.Tag.Name)
                .Select(x => new TagAssignmentDto(
                    x.TagId, x.Tag.Name, x.Tag.Color))
                .ToListAsync(cancellationToken),

            _ => throw new ArgumentException(
                "Unsupported record type.", nameof(recordType))
        };
    }

    public async Task<bool> HasAssignmentsAsync(
        Guid tagId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.CustomerTags.AnyAsync(x => x.TagId == tagId, cancellationToken)
            || await _dbContext.ContactTags.AnyAsync(x => x.TagId == tagId, cancellationToken)
            || await _dbContext.LeadTags.AnyAsync(x => x.TagId == tagId, cancellationToken)
            || await _dbContext.OpportunityTags.AnyAsync(x => x.TagId == tagId, cancellationToken);
    }

    public async Task<IReadOnlyList<TagUsageDto>> GetUsagesAsync(
        Guid tagId,
        CancellationToken cancellationToken = default)
    {
        var customers = await _dbContext.CustomerTags
            .AsNoTracking()
            .Where(x => x.TagId == tagId)
            .Select(x => new TagUsageDto(
                "Customer",
                x.CustomerId,
                x.Customer.Name))
            .ToListAsync(cancellationToken);

        var contacts = await _dbContext.ContactTags
            .AsNoTracking()
            .Where(x => x.TagId == tagId)
            .Select(x => new TagUsageDto(
                "Contact",
                x.ContactId,
                x.Contact.FirstName + " " + x.Contact.LastName))
            .ToListAsync(cancellationToken);

        var leads = await _dbContext.LeadTags
            .AsNoTracking()
            .Where(x => x.TagId == tagId)
            .Select(x => new TagUsageDto(
                "Lead",
                x.LeadId,
                x.Lead.FirstName + " " + x.Lead.LastName))
            .ToListAsync(cancellationToken);

        var opportunities = await _dbContext.OpportunityTags
            .AsNoTracking()
            .Where(x => x.TagId == tagId)
            .Select(x => new TagUsageDto(
                "Opportunity",
                x.OpportunityId,
                x.Opportunity.Title))
            .ToListAsync(cancellationToken);

        return [.. customers, .. contacts, .. leads, .. opportunities];
    }

}
