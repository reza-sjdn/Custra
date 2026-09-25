using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Customers.Queries.GetCustomerById;

public sealed record CustomerDto(
    Guid Id,
    string Name,
    DateTime CreatedAt);