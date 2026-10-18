using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Tags.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tags.Queries.GetTagUsages;

public record GetTagUsagesQuery(Guid TagId)
    : IRequest<IReadOnlyList<TagUsageDto>>, IAuthorizationRequest
{
}