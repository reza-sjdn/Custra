using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tasks.Commands.ReopenTaskItem;

public sealed class ReopenTaskItemCommandValidator
    : AbstractValidator<ReopenTaskItemCommand>
{
    public ReopenTaskItemCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
