using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Tags.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tags.Queries.GetTagUsages;

public class GetTagUsagesQueryHandler
    : IRequestHandler<GetTagUsagesQuery, IReadOnlyList<TagUsageDto>>
{
    private readonly ITagAssignmentQueries _queries;

    public GetTagUsagesQueryHandler(ITagAssignmentQueries queries)
    {
        _queries = queries;
    }

    public Task<IReadOnlyList<TagUsageDto>> Handle(
        GetTagUsagesQuery request,
        CancellationToken cancellationToken)
    {
        return _queries.GetUsagesAsync(request.TagId, cancellationToken);
    }
}