using Custra.Domain.Activities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Activities.DTOs;

public sealed record ActivityDto(
    Guid Id,
    string Subject,
    string? Description,
    ActivityType Type,
    ActivityStatus Status,
    DateTime? DueDate,
    DateTime? CompletedAt,
    Guid OwnerUserId,
    string OwnerName,
    Guid? CustomerId,
    string? CustomerName,
    Guid? ContactId,
    string? ContactName,
    Guid? LeadId,
    string? LeadName,
    Guid? OpportunityId,
    string? OpportunityTitle,
    DateTime CreatedAt);