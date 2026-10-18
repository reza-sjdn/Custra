using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tags.Queries.GetTagLookup;


public record GetTagLookupQuery()
    : IRequest<IReadOnlyList<LookupItemDto>>, IAuthorizationRequest
{
}
