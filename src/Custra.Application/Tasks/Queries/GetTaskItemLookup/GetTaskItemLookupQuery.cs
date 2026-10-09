using Custra.Application.Common.Authorization;
using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Models;
using Custra.Domain.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tasks.Queries.GetTaskItemLookup;

public sealed record GetTaskItemLookupQuery
    : IQuery<IReadOnlyList<LookupItemDto>>, IAuthorizationRequest
{
}