using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Activities.Commands.CreateActivity;

public sealed class CreateActivityCommandValidator
    : AbstractValidator<CreateActivityCommand>
{
    public CreateActivityCommandValidator()
    {
        RuleFor(x => x.Subject)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .MaximumLength(2000);

        RuleFor(x => x.OwnerUserId)
            .NotEmpty();

        RuleFor(x => x.CustomerId)
            .NotEqual(Guid.Empty)
            .When(x => x.CustomerId.HasValue);

        RuleFor(x => x.ContactId)
            .NotEqual(Guid.Empty)
            .When(x => x.ContactId.HasValue);

        RuleFor(x => x.LeadId)
            .NotEqual(Guid.Empty)
            .When(x => x.LeadId.HasValue);

        RuleFor(x => x.OpportunityId)
            .NotEqual(Guid.Empty)
            .When(x => x.OpportunityId.HasValue);

        RuleFor(x => x.Type)
            .IsInEnum();
    }
}