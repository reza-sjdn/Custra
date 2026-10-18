using Custra.Application.Common.Interfaces.Authorization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tags.Commands.CreateTag;


public record CreateTagCommand(string Name, string Color)
    : IRequest<Guid>, IAuthorizationRequest
{
}
