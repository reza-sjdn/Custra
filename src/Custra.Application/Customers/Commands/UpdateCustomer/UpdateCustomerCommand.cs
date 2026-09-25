using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Customers.Commands.UpdateCustomer;

public sealed record UpdateCustomerCommand(
    Guid Id,
    string Name)
    : ICommand<Unit>, IAuthorizationRequest;