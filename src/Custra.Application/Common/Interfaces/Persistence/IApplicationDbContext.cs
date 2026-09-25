using Custra.Domain.Customers;

namespace Custra.Application.Common.Interfaces.Persistence;

public interface IApplicationDbContext
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

    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default);
}