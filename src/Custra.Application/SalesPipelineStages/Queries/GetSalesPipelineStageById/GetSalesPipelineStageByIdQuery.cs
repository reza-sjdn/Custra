using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.SalesPipelineStages.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.SalesPipelineStages.Queries.GetSalesPipelineStageById;

public sealed record GetSalesPipelineStageByIdQuery(
    Guid Id)
    : IQuery<SalesPipelineStageDto>,
      IAuthorizationRequest;
