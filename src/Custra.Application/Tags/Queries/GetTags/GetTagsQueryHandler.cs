using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Common.Models;
using Custra.Application.Tags.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tags.Queries.GetTags;


public class GetTagsQueryHandler
    : IRequestHandler<GetTagsQuery, PagedResult<TagDto>>
{
    private readonly ITagQueries _queries;

    public GetTagsQueryHandler(ITagQueries queries)
    {
        _queries = queries;
    }

    public Task<PagedResult<TagDto>> Handle(
        GetTagsQuery request,
        CancellationToken cancellationToken)
    {
        return _queries.GetPagedAsync(
            request.Search,
            request.Page,
            request.PageSize,
            cancellationToken);
    }
}
