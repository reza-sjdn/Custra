using Custra.Application.Common.Interfaces.Authorization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tags.Commands.RemoveTagAssignment;


public record RemoveTagAssignmentCommand(
    string RecordType,
    Guid RecordId,
    Guid TagId) : IRequest<Unit>, IAuthorizationRequest
{
}
