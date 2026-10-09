using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Notes.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Notes.Queries.GetNoteById;


public sealed record GetNoteByIdQuery(Guid Id)
    : IRequest<NoteDto?>, IAuthorizationRequest
{
    public string RequiredPermission => "Notes.View";
}