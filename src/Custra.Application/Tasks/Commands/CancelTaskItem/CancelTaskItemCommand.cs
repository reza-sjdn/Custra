using Custra.Application.Common.Authorization;
using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using Custra.Domain.Authorization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tasks.Commands.CancelTaskItem;

public sealed record CancelTaskItemCommand(Guid Id)
    : ICommand<Unit>, IAuthorizationRequest
{
}