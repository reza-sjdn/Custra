using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Tags.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tags.Queries.GetTagById;


public record GetTagByIdQuery(Guid Id)
    : IRequest<TagDto?>, IAuthorizationRequest
{
}
