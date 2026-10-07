using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.SalesPipelines.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.SalesPipelines.Queries.GetSalesPipelineById;

public sealed record GetSalesPipelineByIdQuery(Guid Id)
    : IQuery<SalesPipelineDto>,
      IAuthorizationRequest;