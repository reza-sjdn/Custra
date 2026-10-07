using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.SalesPipelineStages.Queries.GetSalesPipelineStageLookup;

public sealed record GetSalesPipelineStageLookupQuery(
    Guid SalesPipelineId)
    : IQuery<IReadOnlyList<LookupItemDto>>,
      IAuthorizationRequest;