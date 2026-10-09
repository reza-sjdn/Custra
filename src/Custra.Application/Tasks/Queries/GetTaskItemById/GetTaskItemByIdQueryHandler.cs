using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Application.Tasks.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tasks.Queries.GetTaskItemById;

public sealed class GetTaskItemByIdQueryHandler
    : IRequestHandler<GetTaskItemByIdQuery, TaskItemDto?>
{
    private readonly ITaskItemQueries _queries;

    public GetTaskItemByIdQueryHandler(ITaskItemQueries queries)
    {
        _queries = queries;
    }

    public Task<TaskItemDto?> Handle(
        GetTaskItemByIdQuery request,
        CancellationToken cancellationToken)
    {
        return _queries.GetByIdAsync(request.Id, cancellationToken);
    }
}