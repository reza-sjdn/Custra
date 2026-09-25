using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Customers.Commands.CreateCustomer;

public sealed record CreateCustomerCommand(
    string Name
) : ICommand<Guid>, IAuthorizationRequest;
