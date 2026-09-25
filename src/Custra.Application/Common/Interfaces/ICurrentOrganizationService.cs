namespace Custra.Application.Common.Interfaces;

public interface ICurrentOrganizationService
{
    Task<Guid> GetOrganizationIdAsync(
        CancellationToken cancellationToken = default);
}