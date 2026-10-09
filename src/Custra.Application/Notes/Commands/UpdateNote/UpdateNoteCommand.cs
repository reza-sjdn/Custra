using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Notes.Commands.UpdateNote;


public sealed record UpdateNoteCommand(
    Guid Id,
    string Title,
    string Content,
    Guid? CustomerId,
    Guid? ContactId,
    Guid? LeadId,
    Guid? OpportunityId)
    : IRequest<Unit>, IAuthorizationRequest
{
}

public sealed class UpdateNoteCommandValidator
    : AbstractValidator<UpdateNoteCommand>
{
    public UpdateNoteCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Content).NotEmpty().MaximumLength(10000);

        RuleFor(x => x)
            .Must(x => x.CustomerId.HasValue ||
                       x.ContactId.HasValue ||
                       x.LeadId.HasValue ||
                       x.OpportunityId.HasValue)
            .WithMessage("A note must be associated with at least one CRM record.");

        RuleFor(x => x)
            .Must(x => !x.ContactId.HasValue || x.CustomerId.HasValue)
            .WithMessage("A customer must be selected when a contact is selected.");

        RuleFor(x => x.CustomerId)
            .Must(x => !x.HasValue || x.Value != Guid.Empty);

        RuleFor(x => x.ContactId)
            .Must(x => !x.HasValue || x.Value != Guid.Empty);

        RuleFor(x => x.LeadId)
            .Must(x => !x.HasValue || x.Value != Guid.Empty);

        RuleFor(x => x.OpportunityId)
            .Must(x => !x.HasValue || x.Value != Guid.Empty);
    }
}

public sealed class UpdateNoteCommandHandler
    : IRequestHandler<UpdateNoteCommand, Unit>
{
    private readonly INoteCommands _commands;
    private readonly ICustomerQueries _customers;
    private readonly IContactQueries _contacts;
    private readonly ILeadQueries _leads;
    private readonly IOpportunityQueries _opportunities;
    private readonly IApplicationDbContext _dbContext;

    public UpdateNoteCommandHandler(
        INoteCommands commands,
        ICustomerQueries customers,
        IContactQueries contacts,
        ILeadQueries leads,
        IOpportunityQueries opportunities,
        IApplicationDbContext dbContext)
    {
        _commands = commands;
        _customers = customers;
        _contacts = contacts;
        _leads = leads;
        _opportunities = opportunities;
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(
        UpdateNoteCommand request,
        CancellationToken cancellationToken)
    {
        var note = await _commands.FindAsync(request.Id, cancellationToken);

        if (note is null)
            throw new KeyNotFoundException("Note not found.");

        if (request.CustomerId.HasValue &&
            !await _customers.ExistsAsync(
                request.CustomerId.Value, cancellationToken))
        {
            throw new ValidationException("The selected customer does not exist.");
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
            !await _leads.ExistsAsync(
                request.LeadId.Value, cancellationToken))
        {
            throw new ValidationException("The selected lead does not exist.");
        }

        if (request.OpportunityId.HasValue &&
            !await _opportunities.ExistsAsync(
                request.OpportunityId.Value, cancellationToken))
        {
            throw new ValidationException("The selected opportunity does not exist.");
        }

        note.Update(
            request.Title,
            request.Content,
            request.CustomerId,
            request.ContactId,
            request.LeadId,
            request.OpportunityId);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}