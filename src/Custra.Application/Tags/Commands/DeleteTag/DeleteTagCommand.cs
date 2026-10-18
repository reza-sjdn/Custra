using Custra.Application.Common.Interfaces.Authorization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tags.Commands.DeleteTag;


public record DeleteTagCommand(Guid Id)
    : IRequest<Unit>, IAuthorizationRequest
{
}
