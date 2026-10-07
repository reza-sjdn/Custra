using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.SalesPipelines.Queries.GetSalesPipelineLookup;

public sealed record GetSalesPipelineLookupQuery
    : IQuery<IReadOnlyList<LookupItemDto>>,
      IAuthorizationRequest;