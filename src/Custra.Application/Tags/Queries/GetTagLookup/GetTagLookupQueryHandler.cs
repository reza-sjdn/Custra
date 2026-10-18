using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tags.Queries.GetTagLookup;


public class GetTagLookupQueryHandler
    : IRequestHandler<GetTagLookupQuery, IReadOnlyList<LookupItemDto>>
{
    private readonly ITagQueries _queries;

    public GetTagLookupQueryHandler(ITagQueries queries)
    {
        _queries = queries;
    }

    public Task<IReadOnlyList<LookupItemDto>> Handle(
        GetTagLookupQuery request,
        CancellationToken cancellationToken)
    {
        return _queries.GetLookupAsync(cancellationToken);
    }
}
