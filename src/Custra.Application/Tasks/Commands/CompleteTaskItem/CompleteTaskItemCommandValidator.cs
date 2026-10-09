using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tasks.Commands.CompleteTaskItem;

public sealed class CompleteTaskItemCommandValidator
    : AbstractValidator<CompleteTaskItemCommand>
{
    public CompleteTaskItemCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
