using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.SalesPipelineStages.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.SalesPipelineStages.Queries.GetSalesPipelineStages;

public sealed record GetSalesPipelineStagesQuery(
    Guid SalesPipelineId)
    : IQuery<IReadOnlyList<SalesPipelineStageDto>>,
      IAuthorizationRequest;