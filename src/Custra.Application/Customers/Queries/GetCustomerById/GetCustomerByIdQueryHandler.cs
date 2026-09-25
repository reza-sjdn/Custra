using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Customers.Queries.GetCustomerById;

public sealed class GetCustomerByIdQueryHandler(
    ICustomerQueries customerQueries)
    : IQueryHandler<GetCustomerByIdQuery, CustomerDto>
{
    private readonly ICustomerQueries _customerQueries = customerQueries;

    public async Task<CustomerDto> Handle(
        GetCustomerByIdQuery request,
        CancellationToken cancellationToken)
    {
        var customer = await _customerQueries.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (customer is null)
            throw new KeyNotFoundException("Customer not found.");

        return customer;
    }
}