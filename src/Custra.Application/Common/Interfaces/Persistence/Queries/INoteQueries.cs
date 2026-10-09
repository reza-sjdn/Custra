using Custra.Application.Common.Models;
using Custra.Application.Notes.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Interfaces.Persistence.Queries;


public interface INoteQueries
{
    Task<NoteDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<PagedResult<NoteDto>> GetPagedAsync(
        string? search = null,
        Guid? customerId = null,
        Guid? contactId = null,
        Guid? leadId = null,
        Guid? opportunityId = null,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default);
}