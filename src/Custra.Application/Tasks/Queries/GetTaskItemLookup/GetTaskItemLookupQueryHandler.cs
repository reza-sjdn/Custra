using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tasks.Queries.GetTaskItemLookup;

public sealed class GetTaskItemLookupQueryHandler
    : IRequestHandler<GetTaskItemLookupQuery, IReadOnlyList<LookupItemDto>>
{
    private readonly ITaskItemQueries _queries;

    public GetTaskItemLookupQueryHandler(ITaskItemQueries queries)
    {
        _queries = queries;
    }

    public Task<IReadOnlyList<LookupItemDto>> Handle(
        GetTaskItemLookupQuery request,
        CancellationToken cancellationToken)
    {
        return _queries.GetLookupAsync(cancellationToken);
    }
}