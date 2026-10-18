using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tags.Commands.AssignTag;


public class AssignTagCommandHandler : IRequestHandler<AssignTagCommand, Unit>
{
    private readonly ITagAssignmentCommands _assignments;
    private readonly ITagQueries _tags;
    private readonly ICustomerQueries _customers;
    private readonly IContactQueries _contacts;
    private readonly ILeadQueries _leads;
    private readonly IOpportunityQueries _opportunities;
    private readonly IApplicationDbContext _dbContext;

    public AssignTagCommandHandler(
        ITagAssignmentCommands assignments,
        ITagQueries tags,
        ICustomerQueries customers,
        IContactQueries contacts,
        ILeadQueries leads,
        IOpportunityQueries opportunities,
        IApplicationDbContext dbContext)
    {
        _assignments = assignments;
        _tags = tags;
        _customers = customers;
        _contacts = contacts;
        _leads = leads;
        _opportunities = opportunities;
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(
        AssignTagCommand request,
        CancellationToken cancellationToken)
    {
        var tag = await _tags.GetByIdAsync(request.TagId, cancellationToken);

        if (tag is null)
            throw new KeyNotFoundException("Tag not found.");

        var recordExists = request.RecordType switch
        {
            "Customer" => await _customers.ExistsAsync(
                request.RecordId, cancellationToken),
            "Contact" => await _contacts.ExistsAsync(
                request.RecordId, cancellationToken),
            "Lead" => await _leads.ExistsAsync(
                request.RecordId, cancellationToken),
            "Opportunity" => await _opportunities.ExistsAsync(
                request.RecordId, cancellationToken),
            _ => false
        };

        if (!recordExists)
            throw new KeyNotFoundException("CRM record not found.");

        if (await _assignments.ExistsAsync(
            request.RecordType, request.RecordId, request.TagId,
            cancellationToken))
        {
            throw new FluentValidation.ValidationException(
                "This tag is already assigned to the record.");
        }

        await _assignments.AddAsync(
            request.RecordType, request.RecordId, request.TagId,
            cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
