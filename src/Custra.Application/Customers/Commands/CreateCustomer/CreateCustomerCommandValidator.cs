using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Customers.Commands.CreateCustomer;

public sealed class CreateCustomerCommandValidator
    : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);
    }
}