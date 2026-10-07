using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.SalesPipelines.Commands.CreateSalesPipeline;

public sealed record CreateSalesPipelineCommand(
    string Name)
    : ICommand<Guid>,
      IAuthorizationRequest;