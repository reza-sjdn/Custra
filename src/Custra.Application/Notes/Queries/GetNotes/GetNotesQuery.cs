using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Models;
using Custra.Application.Notes.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Notes.Queries.GetNotes;


public sealed record GetNotesQuery(
    string? Search = null,
    Guid? CustomerId = null,
    Guid? ContactId = null,
    Guid? LeadId = null,
    Guid? OpportunityId = null,
    int Page = 1,
    int PageSize = 10)
    : IRequest<PagedResult<NoteDto>>, IAuthorizationRequest
{
    public string RequiredPermission => "Notes.View";
}