using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.SalesPipelines.Commands.DeleteSalesPipeline;

public sealed record DeleteSalesPipelineCommand(
    Guid Id)
    : ICommand<Unit>,
      IAuthorizationRequest;