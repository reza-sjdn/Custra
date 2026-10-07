using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.SalesPipelineStages.Commands.UpdateSalesPipelineStage;

public sealed class UpdateSalesPipelineStageCommandValidator
    : AbstractValidator<UpdateSalesPipelineStageCommand>
{
    public UpdateSalesPipelineStageCommandValidator()
    {
        RuleFor(x => x.Id)
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