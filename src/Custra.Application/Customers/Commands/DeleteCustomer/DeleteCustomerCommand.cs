using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Customers.Commands.DeleteCustomer;

public sealed record DeleteCustomerCommand(
    Guid Id)
    : ICommand<Unit>, IAuthorizationRequest;