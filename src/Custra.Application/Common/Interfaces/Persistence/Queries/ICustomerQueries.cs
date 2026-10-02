using Custra.Application.Common.Models;
using Custra.Application.Customers.Queries.GetCustomerById;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Interfaces.Persistence.Queries;

public interface ICustomerQueries
{
    Task<CustomerDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<PagedResult<CustomerDto>> GetPagedAsync(
        string? search,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);
}