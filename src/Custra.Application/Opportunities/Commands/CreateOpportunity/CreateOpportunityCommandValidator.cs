using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Opportunities.Commands.CreateOpportunity;

public sealed class CreateOpportunityCommandValidator
    : AbstractValidator<CreateOpportunityCommand>
{
    public CreateOpportunityCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty();

        RuleFor(x => x.OwnerUserId)
            .NotEmpty();

        RuleFor(x => x.SalesPipelineId)
            .NotEmpty();

        RuleFor(x => x.SalesPipelineStageId)
            .NotEmpty();

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .MaximumLength(2000);

        RuleFor(x => x.EstimatedValue)
            .GreaterThanOrEqualTo(0)
            .When(x => x.EstimatedValue.HasValue);
    }
}