using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.SalesPipelineStages.Commands.UpdateSalesPipelineStage;

public sealed record UpdateSalesPipelineStageCommand(
    Guid Id,
    string Name,
    int Order,
    decimal? Probability)
    : ICommand<Unit>,
      IAuthorizationRequest;