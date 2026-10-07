using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Domain.Opportunities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Custra.Application.Opportunities.Commands.CreateOpportunity;

public sealed class CreateOpportunityCommandHandler
    : ICommandHandler<CreateOpportunityCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IOpportunityCommands _opportunityCommands;
    private readonly IOpportunityQueries _opportunityQueries;
    private readonly ISalesPipelineCommands _pipelineCommands;
    private readonly ICustomerQueries _customerQueries;
    private readonly IContactQueries _contactQueries;
    private readonly IOrganizationUserQueries _organizationUserQueries;

    public CreateOpportunityCommandHandler(
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

    public async Task<Guid> Handle(
        CreateOpportunityCommand request,
        CancellationToken cancellationToken)
    {
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

        var opportunity = new Opportunity(
            request.CustomerId,
            request.ContactId,
            request.OwnerUserId,
            request.SalesPipelineId,
            request.SalesPipelineStageId,
            request.Title,
            request.Description,
            request.EstimatedValue,
            request.ExpectedCloseDate);

        await _opportunityCommands.AddAsync(
            opportunity,
            cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return opportunity.Id;
    }
}