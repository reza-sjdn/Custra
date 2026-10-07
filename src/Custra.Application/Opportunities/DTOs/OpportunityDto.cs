using Custra.Domain.Opportunities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Opportunities.DTOs;

public sealed record OpportunityDto(
    Guid Id,
    Guid CustomerId,
    Guid? ContactId,
    Guid OwnerUserId,
    Guid SalesPipelineId,
    Guid SalesPipelineStageId,
    string Title,
    string? Description,
    decimal? EstimatedValue,
    DateTime? ExpectedCloseDate,
    OpportunityStatus Status,
    DateTime CreatedAt);