using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Activities.Commands.CompleteActivity;

public sealed record CompleteActivityCommand(Guid Id)
    : ICommand<Unit>,
      IAuthorizationRequest;