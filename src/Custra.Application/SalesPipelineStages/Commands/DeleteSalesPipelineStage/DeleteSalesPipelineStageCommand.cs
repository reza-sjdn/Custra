using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.SalesPipelineStages.Commands.DeleteSalesPipelineStage;

public sealed record DeleteSalesPipelineStageCommand(
    Guid Id)
    : ICommand<Unit>,
      IAuthorizationRequest;