using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Common.Models;
using Custra.Application.Customers.Queries.GetCustomerById;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Customers.Queries.GetCustomers;

public sealed class GetCustomersQueryHandler(
    ICustomerQueries customerQueries)
    : IQueryHandler<GetCustomersQuery, PagedResult<CustomerDto>>
{
    private readonly ICustomerQueries _customerQueries = customerQueries;

    public async Task<PagedResult<CustomerDto>> Handle(
        GetCustomersQuery request,
        CancellationToken cancellationToken)
    {
        return await _customerQueries.GetPagedAsync(
            request.Search,
            request.Page,
            request.PageSize,
            cancellationToken);
    }
}