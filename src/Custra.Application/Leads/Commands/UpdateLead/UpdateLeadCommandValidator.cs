using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Leads.Commands.UpdateLead;

public sealed class UpdateLeadCommandValidator
    : AbstractValidator<UpdateLeadCommand>
{
    public UpdateLeadCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.CompanyName)
            .MaximumLength(200);

        RuleFor(x => x.JobTitle)
            .MaximumLength(150);

        RuleFor(x => x.Email)
            .MaximumLength(320)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.Phone)
            .MaximumLength(50);
    }
}