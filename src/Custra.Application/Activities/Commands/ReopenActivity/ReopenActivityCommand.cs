using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Activities.Commands.ReopenActivity;

public sealed record ReopenActivityCommand(Guid Id)
    : ICommand<Unit>,
      IAuthorizationRequest;