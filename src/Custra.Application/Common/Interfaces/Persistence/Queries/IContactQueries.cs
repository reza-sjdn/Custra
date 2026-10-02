using Custra.Application.Common.Models;
using Custra.Application.Contacts.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Interfaces.Persistence.Queries;

public interface IContactQueries
{
    Task<ContactDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    // Notice that the list is customer-specific.We don't want an arbitrary organization-wide contact list at this stage.
    Task<PagedResult<ContactDto>> GetPagedAsync(
        Guid customerId,
        string? search,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);
}