using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Common.Models;
using Custra.Application.Notes.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Notes.Queries.GetNotes;


public sealed class GetNotesQueryHandler
    : IRequestHandler<GetNotesQuery, PagedResult<NoteDto>>
{
    private readonly INoteQueries _queries;

    public GetNotesQueryHandler(INoteQueries queries)
    {
        _queries = queries;
    }

    public Task<PagedResult<NoteDto>> Handle(
        GetNotesQuery request,
        CancellationToken cancellationToken)
    {
        return _queries.GetPagedAsync(
            request.Search,
            request.CustomerId,
            request.ContactId,
            request.LeadId,
            request.OpportunityId,
            request.Page,
            request.PageSize,
            cancellationToken);
    }
}