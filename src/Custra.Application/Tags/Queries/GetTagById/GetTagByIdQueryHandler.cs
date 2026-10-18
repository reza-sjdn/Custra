using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Tags.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tags.Queries.GetTagById;


public class GetTagByIdQueryHandler
    : IRequestHandler<GetTagByIdQuery, TagDto?>
{
    private readonly ITagQueries _queries;

    public GetTagByIdQueryHandler(ITagQueries queries)
    {
        _queries = queries;
    }

    public Task<TagDto?> Handle(
        GetTagByIdQuery request,
        CancellationToken cancellationToken)
    {
        return _queries.GetByIdAsync(request.Id, cancellationToken);
    }
}
