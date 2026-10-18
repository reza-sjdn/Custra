using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tags.Commands.RemoveTagAssignment;


public class RemoveTagAssignmentCommandValidator
    : AbstractValidator<RemoveTagAssignmentCommand>
{
    public RemoveTagAssignmentCommandValidator()
    {
        RuleFor(x => x.RecordType)
            .Must(x => x is "Customer" or "Contact" or "Lead" or "Opportunity")
            .WithMessage("Unsupported record type.");

        RuleFor(x => x.RecordId).NotEmpty();
        RuleFor(x => x.TagId).NotEmpty();
    }
}
