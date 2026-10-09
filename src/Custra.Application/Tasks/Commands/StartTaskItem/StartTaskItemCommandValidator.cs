using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tasks.Commands.StartTaskItem;

public sealed class StartTaskItemCommandValidator
    : AbstractValidator<StartTaskItemCommand>
{
    public StartTaskItemCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
