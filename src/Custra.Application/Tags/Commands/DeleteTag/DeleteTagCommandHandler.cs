using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tags.Commands.DeleteTag;


public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand, Unit>
{
    private readonly ITagCommands _commands;
    private readonly IApplicationDbContext _dbContext;
    private readonly ITagAssignmentQueries _assignmentQueries;

    public DeleteTagCommandHandler(
        ITagCommands commands,
        IApplicationDbContext dbContext,
        ITagAssignmentQueries assignmentQueries)
    {
        _commands = commands;
        _dbContext = dbContext;
        _assignmentQueries = assignmentQueries;
    }

    public async Task<Unit> Handle(
        DeleteTagCommand request,
        CancellationToken cancellationToken)
    {
        var tag = await _commands.FindAsync(request.Id, cancellationToken);

        if (tag is null)
            throw new KeyNotFoundException("Tag not found.");

        bool hasAnyAssignedRecord = await _assignmentQueries.HasAssignmentsAsync(
            tag.Id, cancellationToken);

        if (hasAnyAssignedRecord)
        {
            throw new ValidationException(
                "This tag cannot be deleted because it is assigned to one or more records.");
        }

        _commands.Delete(tag);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
