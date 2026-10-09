using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tasks.Commands.UpdateTaskItem;

public sealed class UpdateTaskItemCommandHandler
    : IRequestHandler<UpdateTaskItemCommand, Unit>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ITaskItemCommands _commands;
    private readonly IOrganizationUserQueries _users;
    private readonly ICustomerQueries _customers;
    private readonly IContactQueries _contacts;
    private readonly ILeadQueries _leads;
    private readonly IOpportunityQueries _opportunities;
    private readonly IActivityQueries _activities;

    public UpdateTaskItemCommandHandler(
        IApplicationDbContext dbContext,
        ITaskItemCommands commands,
        IOrganizationUserQueries users,
        ICustomerQueries customers,
        IContactQueries contacts,
        ILeadQueries leads,
        IOpportunityQueries opportunities,
        IActivityQueries activities)
    {
        _dbContext = dbContext;
        _commands = commands;
        _users = users;
        _customers = customers;
        _contacts = contacts;
        _leads = leads;
        _opportunities = opportunities;
        _activities = activities;
    }

    public async Task<Unit> Handle(
        UpdateTaskItemCommand request,
        CancellationToken cancellationToken)
    {
        var task = await _commands.FindAsync(request.Id, cancellationToken);

        if (task is null)
        {
            throw new KeyNotFoundException("Task was not found.");
        }

        if (!await _users.ExistsAsync(request.OwnerUserId, cancellationToken))
        {
            throw new ValidationException(
                "The selected task owner is not a member of this organization.");
        }

        if (request.CustomerId.HasValue &&
            !await _customers.ExistsAsync(request.CustomerId.Value, cancellationToken))
        {
            throw new ValidationException("The selected customer was not found.");
        }

        if (request.ContactId.HasValue &&
            !await _contacts.BelongsToCustomerAsync(
                request.ContactId.Value,
                request.CustomerId!.Value,
                cancellationToken))
        {
            throw new ValidationException(
                "The selected contact does not belong to the selected customer.");
        }

        if (request.LeadId.HasValue &&
            !await _leads.ExistsAsync(request.LeadId.Value, cancellationToken))
        {
            throw new ValidationException("The selected lead was not found.");
        }

        if (request.OpportunityId.HasValue &&
            !await _opportunities.ExistsAsync(
                request.OpportunityId.Value,
                cancellationToken))
        {
            throw new ValidationException("The selected opportunity was not found.");
        }

        if (request.ActivityId.HasValue &&
            await _activities.GetByIdAsync(
                request.ActivityId.Value,
                cancellationToken) is null)
        {
            throw new ValidationException("The selected activity was not found.");
        }

        task.Update(
            request.Title,
            request.OwnerUserId,
            request.Priority,
            request.Description,
            request.DueDate,
            request.CustomerId,
            request.ContactId,
            request.LeadId,
            request.OpportunityId,
            request.ActivityId);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}