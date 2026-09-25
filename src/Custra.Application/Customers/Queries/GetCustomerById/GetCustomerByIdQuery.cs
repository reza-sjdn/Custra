using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Customers.Queries.GetCustomerById;

public sealed record GetCustomerByIdQuery(Guid Id) : IQuery<CustomerDto>, IAuthorizationRequest;