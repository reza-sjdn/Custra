using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tasks.Commands.DeleteTaskItem;

public sealed class DeleteTaskItemCommandValidator
    : AbstractValidator<DeleteTaskItemCommand>
{
    public DeleteTaskItemCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}