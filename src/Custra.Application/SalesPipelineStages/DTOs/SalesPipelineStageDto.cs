using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.SalesPipelineStages.DTOs;

public sealed record SalesPipelineStageDto(
    Guid Id,
    Guid SalesPipelineId,
    string Name,
    int Order,
    decimal? Probability);