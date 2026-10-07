using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Customers.Queries.GetCustomerLookup;

public sealed class GetCustomerLookupQueryHandler
    : IQueryHandler<
        GetCustomerLookupQuery,
        IReadOnlyList<LookupItemDto>>
{
    private readonly ICustomerQueries _customerQueries;

    public GetCustomerLookupQueryHandler(
        ICustomerQueries customerQueries)
    {
        _customerQueries = customerQueries;
    }

    public Task<IReadOnlyList<LookupItemDto>> Handle(
        GetCustomerLookupQuery request,
        CancellationToken cancellationToken)
    {
        return _customerQueries.GetLookupAsync(cancellationToken);
    }
}