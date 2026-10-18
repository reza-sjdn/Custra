using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tags.Commands.UpdateTag;


public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, Unit>
{
    private readonly ITagCommands _commands;
    private readonly ITagQueries _queries;
    private readonly IApplicationDbContext _dbContext;

    public UpdateTagCommandHandler(
        ITagCommands commands,
        ITagQueries queries,
        IApplicationDbContext dbContext)
    {
        _commands = commands;
        _queries = queries;
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(
        UpdateTagCommand request,
        CancellationToken cancellationToken)
    {
        var tag = await _commands.FindAsync(request.Id, cancellationToken);

        if (tag is null)
            throw new KeyNotFoundException("Tag not found.");

        if (await _queries.ExistsByNameAsync(
            request.Name,
            excludeId: request.Id,
            cancellationToken: cancellationToken))
        {
            throw new FluentValidation.ValidationException(
                "A tag with this name already exists.");
        }

        tag.Update(request.Name, request.Color);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
