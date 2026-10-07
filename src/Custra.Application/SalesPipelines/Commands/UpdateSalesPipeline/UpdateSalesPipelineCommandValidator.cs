using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.SalesPipelines.Commands.UpdateSalesPipeline;

public sealed class UpdateSalesPipelineCommandValidator
    : AbstractValidator<UpdateSalesPipelineCommand>
{
    public UpdateSalesPipelineCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}