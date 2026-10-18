using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tags.Commands.RemoveTagAssignment;


public class RemoveTagAssignmentCommandHandler
    : IRequestHandler<RemoveTagAssignmentCommand, Unit>
{
    private readonly ITagAssignmentCommands _assignments;
    private readonly IApplicationDbContext _dbContext;

    public RemoveTagAssignmentCommandHandler(
        ITagAssignmentCommands assignments,
        IApplicationDbContext dbContext)
    {
        _assignments = assignments;
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(
        RemoveTagAssignmentCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await _assignments.ExistsAsync(
            request.RecordType,
            request.RecordId,
            request.TagId,
            cancellationToken);

        if (!exists)
            throw new KeyNotFoundException("Tag assignment not found.");

        await _assignments.RemoveAsync(
            request.RecordType,
            request.RecordId,
            request.TagId,
            cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
