using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tasks.Commands.CreateTaskItem;

public sealed class CreateTaskItemCommandValidator
    : AbstractValidator<CreateTaskItemCommand>
{
    public CreateTaskItemCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .MaximumLength(2000);

        RuleFor(x => x.Priority)
            .IsInEnum();

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

        RuleFor(x => x.ActivityId)
            .NotEqual(Guid.Empty)
            .When(x => x.ActivityId.HasValue);

        RuleFor(x => x)
            .Must(x => !x.ContactId.HasValue || x.CustomerId.HasValue)
            .WithMessage("A customer must be selected when a contact is selected.");
    }
}