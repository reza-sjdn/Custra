using Custra.Application.Common.Interfaces.Authorization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tags.Commands.UpdateTag;


public record UpdateTagCommand(Guid Id, string Name, string Color)
    : IRequest<Unit>, IAuthorizationRequest
{
}
