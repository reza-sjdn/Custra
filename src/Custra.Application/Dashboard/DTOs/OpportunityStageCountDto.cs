using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Dashboard.DTOs;

public sealed record OpportunityStageCountDto(
    Guid StageId,
    string StageName,
    int Count,
    decimal TotalValue);