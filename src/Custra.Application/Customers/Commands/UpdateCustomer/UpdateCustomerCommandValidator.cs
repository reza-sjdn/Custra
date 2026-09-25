using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Customers.Commands.UpdateCustomer;

public sealed class UpdateCustomerCommandValidator
    : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);
    }
}