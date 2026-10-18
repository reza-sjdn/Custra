using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Domain.Tags;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Commands;


public class TagAssignmentCommands : ITagAssignmentCommands
{
    private readonly CustraDbContext _dbContext;

    public TagAssignmentCommands(CustraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> ExistsAsync(
        string recordType,
        Guid recordId,
        Guid tagId,
        CancellationToken cancellationToken = default)
    {
        return recordType switch
        {
            "Customer" => await _dbContext.CustomerTags.AnyAsync(
                x => x.CustomerId == recordId && x.TagId == tagId,
                cancellationToken),

            "Contact" => await _dbContext.ContactTags.AnyAsync(
                x => x.ContactId == recordId && x.TagId == tagId,
                cancellationToken),

            "Lead" => await _dbContext.LeadTags.AnyAsync(
                x => x.LeadId == recordId && x.TagId == tagId,
                cancellationToken),

            "Opportunity" => await _dbContext.OpportunityTags.AnyAsync(
                x => x.OpportunityId == recordId && x.TagId == tagId,
                cancellationToken),

            _ => throw new ArgumentException("Unsupported record type.")
        };
    }

    public async Task AddAsync(
        string recordType,
        Guid recordId,
        Guid tagId,
        CancellationToken cancellationToken = default)
    {
        switch (recordType)
        {
            case "Customer":
                await _dbContext.CustomerTags.AddAsync(
                    new CustomerTag(recordId, tagId), cancellationToken);
                break;

            case "Contact":
                await _dbContext.ContactTags.AddAsync(
                    new ContactTag(recordId, tagId), cancellationToken);
                break;

            case "Lead":
                await _dbContext.LeadTags.AddAsync(
                    new LeadTag(recordId, tagId), cancellationToken);
                break;

            case "Opportunity":
                await _dbContext.OpportunityTags.AddAsync(
                    new OpportunityTag(recordId, tagId), cancellationToken);
                break;

            default:
                throw new ArgumentException("Unsupported record type.");
        }
    }

    public async Task RemoveAsync(
        string recordType,
        Guid recordId,
        Guid tagId,
        CancellationToken cancellationToken = default)
    {
        switch (recordType)
        {
            case "Customer":
                {
                    var assignment = await _dbContext.CustomerTags.FirstOrDefaultAsync(
                        x => x.CustomerId == recordId && x.TagId == tagId,
                        cancellationToken);

                    if (assignment is not null)
                        _dbContext.CustomerTags.Remove(assignment);
                    break;
                }

            case "Contact":
                {
                    var assignment = await _dbContext.ContactTags.FirstOrDefaultAsync(
                        x => x.ContactId == recordId && x.TagId == tagId,
                        cancellationToken);

                    if (assignment is not null)
                        _dbContext.ContactTags.Remove(assignment);
                    break;
                }

            case "Lead":
                {
                    var assignment = await _dbContext.LeadTags.FirstOrDefaultAsync(
                        x => x.LeadId == recordId && x.TagId == tagId,
                        cancellationToken);

                    if (assignment is not null)
                        _dbContext.LeadTags.Remove(assignment);
                    break;
                }

            case "Opportunity":
                {
                    var assignment = await _dbContext.OpportunityTags.FirstOrDefaultAsync(
                        x => x.OpportunityId == recordId && x.TagId == tagId,
                        cancellationToken);

                    if (assignment is not null)
                        _dbContext.OpportunityTags.Remove(assignment);
                    break;
                }

            default:
                throw new ArgumentException("Unsupported record type.");
        }
    }
}
