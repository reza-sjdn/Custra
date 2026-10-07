using Custra.Application.SalesPipelineStages.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.SalesPipelines.DTOs;

public sealed record SalesPipelineDto(
    Guid Id,
    string Name,
    IReadOnlyList<SalesPipelineStageDto> Stages);