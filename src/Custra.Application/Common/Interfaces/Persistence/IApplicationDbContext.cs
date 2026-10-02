using Custra.Domain.Customers;

namespace Custra.Application.Common.Interfaces.Persistence;

public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default);
}