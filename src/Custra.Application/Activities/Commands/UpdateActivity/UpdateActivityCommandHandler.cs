using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Activities.Commands.UpdateActivity;

public sealed class UpdateActivityCommandHandler
    : ICommandHandler<UpdateActivityCommand, Unit>
{
    private readonly IActivityCommands _activityCommands;
    private readonly IApplicationDbContext _dbContext;
    private readonly IOrganizationUserQueries _organizationUserQueries;
    private readonly ICustomerQueries _customerQueries;
    private readonly IContactQueries _contactQueries;
    private readonly ILeadQueries _leadQueries;
    private readonly IOpportunityQueries _opportunityQueries;

    public UpdateActivityCommandHandler(
        IActivityCommands activityCommands,
        IApplicationDbContext dbContext,
        IOrganizationUserQueries organizationUserQueries,
        ICustomerQueries customerQueries,
        IContactQueries contactQueries,
        ILeadQueries leadQueries,
        IOpportunityQueries opportunityQueries)
    {
        _activityCommands = activityCommands;
        _dbContext = dbContext;
        _organizationUserQueries = organizationUserQueries;
        _customerQueries = customerQueries;
        _contactQueries = contactQueries;
        _leadQueries = leadQueries;
        _opportunityQueries = opportunityQueries;
    }

    public async Task<Unit> Handle(
        UpdateActivityCommand request,
        CancellationToken cancellationToken)
    {
        var activity = await _activityCommands.FindAsync(
            request.Id,
            cancellationToken);

        if (activity is null)
            throw new KeyNotFoundException("Activity not found.");

        var ownerExists = await _organizationUserQueries.ExistsAsync(
            request.OwnerUserId,
            cancellationToken);

        if (!ownerExists)
        {
            throw new ValidationException(
                "The selected owner is not a member of the current organization.");
        }

        if (request.CustomerId.HasValue)
        {
            var customerExists = await _customerQueries.ExistsAsync(
                request.CustomerId.Value,
                cancellationToken);

            if (!customerExists)
            {
                throw new ValidationException(
                    "The selected customer does not exist.");
            }
        }

        if (request.ContactId.HasValue)
        {
            if (!request.CustomerId.HasValue)
            {
                throw new ValidationException(
                    "A contact requires a customer.");
            }

            var contactBelongsToCustomer =
                await _contactQueries.BelongsToCustomerAsync(
                    request.ContactId.Value,
                    request.CustomerId.Value,
                    cancellationToken);

            if (!contactBelongsToCustomer)
            {
                throw new ValidationException(
                    "The selected contact does not belong to the selected customer.");
            }
        }

        if (request.LeadId.HasValue)
        {
            var leadExists = await _leadQueries.ExistsAsync(
                request.LeadId.Value,
                cancellationToken);

            if (!leadExists)
            {
                throw new ValidationException(
                    "The selected lead does not exist.");
            }
        }

        if (request.OpportunityId.HasValue)
        {
            var opportunityExists = await _opportunityQueries.ExistsAsync(
                request.OpportunityId.Value,
                cancellationToken);

            if (!opportunityExists)
            {
                throw new ValidationException(
                    "The selected opportunity does not exist.");
            }
        }

        activity.Update(
            request.Subject,
            request.Type,
            request.OwnerUserId,
            request.DueDate,
            request.Description,
            request.CustomerId,
            request.ContactId,
            request.LeadId,
            request.OpportunityId);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}