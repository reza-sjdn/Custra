using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Opportunities.Commands.UpdateOpportunity;

public sealed class UpdateOpportunityCommandHandler
    : ICommandHandler<UpdateOpportunityCommand, Unit>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IOpportunityCommands _opportunityCommands;
    private readonly IOpportunityQueries _opportunityQueries;
    private readonly ISalesPipelineCommands _pipelineCommands;
    private readonly ICustomerQueries _customerQueries;
    private readonly IContactQueries _contactQueries;
    private readonly IOrganizationUserQueries _organizationUserQueries;

    public UpdateOpportunityCommandHandler(
        IApplicationDbContext dbContext,
        IOpportunityCommands opportunityCommands,
        IOpportunityQueries opportunityQueries,
        ISalesPipelineCommands pipelineCommands,
        ICustomerQueries customerQueries,
        IContactQueries contactQueries,
        IOrganizationUserQueries organizationUserQueries)
    {
        _dbContext = dbContext;
        _opportunityCommands = opportunityCommands;
        _opportunityQueries = opportunityQueries;
        _pipelineCommands = pipelineCommands;
        _customerQueries = customerQueries;
        _contactQueries = contactQueries;
        _organizationUserQueries = organizationUserQueries;
    }

    public async Task<Unit> Handle(
        UpdateOpportunityCommand request,
        CancellationToken cancellationToken)
    {
        var opportunity = await _opportunityCommands.FindAsync(
            request.Id,
            cancellationToken);

        if (opportunity is null)
            throw new KeyNotFoundException(
                "Opportunity was not found.");

        var customerExists = await _customerQueries
            .ExistsAsync(request.CustomerId, cancellationToken);

        if (!customerExists)
            throw new KeyNotFoundException(
                "Customer was not found.");

        if (request.ContactId.HasValue)
        {
            var contactBelongsToCustomer =
                await _contactQueries.BelongsToCustomerAsync(
                    request.ContactId.Value,
                    request.CustomerId,
                    cancellationToken);

            if (!contactBelongsToCustomer)
            {
                throw new ValidationException(
                    "The selected contact does not belong to the selected customer.");
            }
        }

        var pipeline = await _pipelineCommands.FindAsync(
            request.SalesPipelineId,
            cancellationToken);

        if (pipeline is null)
            throw new KeyNotFoundException(
                "Sales pipeline was not found.");

        var stage = pipeline.Stages
            .FirstOrDefault(x =>
                x.Id == request.SalesPipelineStageId);

        if (stage is null)
            throw new KeyNotFoundException(
                "Sales pipeline stage was not found.");

        var stageBelongsToPipeline =
            await _opportunityQueries.StageBelongsToPipelineAsync(
                request.SalesPipelineStageId,
                request.SalesPipelineId,
                cancellationToken);

        if (!stageBelongsToPipeline)
        {
            throw new ValidationException(
                "The selected stage does not belong to the selected pipeline.");
        }

        var ownerExists = await _organizationUserQueries.ExistsAsync(
            request.OwnerUserId,
            cancellationToken);

        if (!ownerExists)
        {
            throw new ValidationException(
                "The selected owner is not a member of the current organization.");
        }

        opportunity.Update(
            request.CustomerId,
            request.ContactId,
            request.OwnerUserId,
            request.SalesPipelineId,
            request.Title,
            request.Description,
            request.EstimatedValue,
            request.ExpectedCloseDate);

        opportunity.ChangeStage(request.SalesPipelineStageId);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}