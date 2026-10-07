using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.SalesPipelineStages.Commands.CreateSalesPipelineStage;

public sealed class CreateSalesPipelineStageCommandValidator
    : AbstractValidator<CreateSalesPipelineStageCommand>
{
    public CreateSalesPipelineStageCommandValidator()
    {
        RuleFor(x => x.SalesPipelineId)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Order)
            .GreaterThan(0);

        RuleFor(x => x.Probability)
            .InclusiveBetween(0, 100)
            .When(x => x.Probability.HasValue);
    }
}