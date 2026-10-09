using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Notes.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Notes.Queries.GetNoteById;


public sealed class GetNoteByIdQueryHandler
    : IRequestHandler<GetNoteByIdQuery, NoteDto?>
{
    private readonly INoteQueries _queries;

    public GetNoteByIdQueryHandler(INoteQueries queries)
    {
        _queries = queries;
    }

    public Task<NoteDto?> Handle(
        GetNoteByIdQuery request,
        CancellationToken cancellationToken)
    {
        return _queries.GetByIdAsync(request.Id, cancellationToken);
    }
}