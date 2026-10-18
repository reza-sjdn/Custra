using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tags.Commands.AssignTag;


public class AssignTagCommandValidator : AbstractValidator<AssignTagCommand>
{
    public AssignTagCommandValidator()
    {
        RuleFor(x => x.RecordType)
            .Must(BeSupportedRecordType)
            .WithMessage("Unsupported record type.");

        RuleFor(x => x.RecordId)
            .NotEmpty();

        RuleFor(x => x.TagId)
            .NotEmpty();
    }

    private static bool BeSupportedRecordType(string recordType) =>
        recordType is "Customer" or "Contact" or "Lead" or "Opportunity";
}
