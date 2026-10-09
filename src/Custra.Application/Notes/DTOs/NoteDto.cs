using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Notes.DTOs;

public sealed record NoteDto(
    Guid Id,
    string Title,
    string Content,
    Guid? CustomerId,
    string? CustomerName,
    Guid? ContactId,
    string? ContactName,
    Guid? LeadId,
    string? LeadName,
    Guid? OpportunityId,
    string? OpportunityTitle,
    DateTime CreatedAt,
    DateTime? ModifiedAt);