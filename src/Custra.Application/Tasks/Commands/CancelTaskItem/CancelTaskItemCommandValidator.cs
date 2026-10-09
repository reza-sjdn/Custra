using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tasks.Commands.CancelTaskItem;

public sealed class CancelTaskItemCommandValidator
    : AbstractValidator<CancelTaskItemCommand>
{
    public CancelTaskItemCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
