using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tags.DTOs;


public record TagDto(
    Guid Id,
    string Name,
    string Color,
    DateTime CreatedAt,
    DateTime? ModifiedAt,
    int? CustomerCount,
    int? ContactCount,
    int? LeadCount,
    int? OpportunityCount,
    int? TotalUsageCount);
