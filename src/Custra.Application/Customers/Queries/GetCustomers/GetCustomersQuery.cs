using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Models;
using Custra.Application.Customers.Queries.GetCustomerById;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Customers.Queries.GetCustomers;

public sealed record GetCustomersQuery(
    string? Search,
    int Page = 1,
    int PageSize = 2)
    : IQuery<PagedResult<CustomerDto>>,
      IAuthorizationRequest;