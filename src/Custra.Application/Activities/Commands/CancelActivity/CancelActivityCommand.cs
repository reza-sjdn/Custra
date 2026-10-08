using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Activities.Commands.CancelActivity;

public sealed record CancelActivityCommand(Guid Id)
    : ICommand<Unit>,
      IAuthorizationRequest;