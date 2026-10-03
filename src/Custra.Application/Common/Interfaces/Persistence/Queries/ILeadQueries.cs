using Custra.Application.Common.Models;
using Custra.Application.Leads.DTOs;
using Custra.Domain.Leads;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Interfaces.Persistence.Queries;

public interface ILeadQueries
{
    Task<LeadDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<PagedResult<LeadDto>> GetPagedAsync(
        string? search,
        LeadStatus? status,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);
}