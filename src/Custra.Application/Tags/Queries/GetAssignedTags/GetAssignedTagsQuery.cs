using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Tags.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tags.Queries.GetAssignedTags;


public record GetAssignedTagsQuery(string RecordType, Guid RecordId)
    : IRequest<IReadOnlyList<TagAssignmentDto>>, IAuthorizationRequest
{
}
