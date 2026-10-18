using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Models;
using Custra.Application.Tags.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tags.Queries.GetTags;


public record GetTagsQuery(
    string? Search = null,
    int Page = 1,
    int PageSize = 10)
    : IRequest<PagedResult<TagDto>>, IAuthorizationRequest
{
}
