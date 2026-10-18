using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Tags.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tags.Queries.GetAssignedTags;


public class GetAssignedTagsQueryHandler
    : IRequestHandler<GetAssignedTagsQuery, IReadOnlyList<TagAssignmentDto>>
{
    private readonly ITagAssignmentQueries _queries;

    public GetAssignedTagsQueryHandler(ITagAssignmentQueries queries)
    {
        _queries = queries;
    }

    public Task<IReadOnlyList<TagAssignmentDto>> Handle(
        GetAssignedTagsQuery request,
        CancellationToken cancellationToken)
    {
        return _queries.GetAssignedTagsAsync(
            request.RecordType, request.RecordId, cancellationToken);
    }
}
