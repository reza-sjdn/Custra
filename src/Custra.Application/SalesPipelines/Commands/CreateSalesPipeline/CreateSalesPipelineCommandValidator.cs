using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.SalesPipelines.Commands.CreateSalesPipeline;

public sealed class CreateSalesPipelineCommandValidator
    : AbstractValidator<CreateSalesPipelineCommand>
{
    public CreateSalesPipelineCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}