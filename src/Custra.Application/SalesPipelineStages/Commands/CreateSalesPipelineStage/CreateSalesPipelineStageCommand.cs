using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.SalesPipelineStages.Commands.CreateSalesPipelineStage;

public sealed record CreateSalesPipelineStageCommand(
    Guid SalesPipelineId,
    string Name,
    int Order,
    decimal? Probability)
    : ICommand<Guid>,
      IAuthorizationRequest;