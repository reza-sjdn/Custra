using Custra.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Interfaces.Persistence.Commands;

public interface ICustomerCommands
{
    Task AddCustomerAsync(
        Customer customer,
        CancellationToken cancellationToken = default);

    Task<Customer?> FindCustomerAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task DeleteCustomerAsync(
        Customer customer,
        CancellationToken cancellationToken = default);

}
