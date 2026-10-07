using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.SalesPipelines.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.SalesPipelines.Queries.GetSalesPipelines;

public sealed record GetSalesPipelinesQuery
    : IQuery<IReadOnlyList<SalesPipelineDto>>,
      IAuthorizationRequest;