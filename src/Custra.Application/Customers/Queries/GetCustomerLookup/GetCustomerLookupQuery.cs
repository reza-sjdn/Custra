using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Customers.Queries.GetCustomerLookup;

public sealed record GetCustomerLookupQuery
    : IQuery<IReadOnlyList<LookupItemDto>>,
      IAuthorizationRequest;
