using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Application.Common.Interfaces.Persistence.Queries;
using Custra.Domain.Tags;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tags.Commands.CreateTag;


public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, Guid>
{
    private readonly ITagCommands _commands;
    private readonly ITagQueries _queries;
    private readonly IApplicationDbContext _dbContext;

    public CreateTagCommandHandler(
        ITagCommands commands,
        ITagQueries queries,
        IApplicationDbContext dbContext)
    {
        _commands = commands;
        _queries = queries;
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(
        CreateTagCommand request,
        CancellationToken cancellationToken)
    {
        if (await _queries.ExistsByNameAsync(
            request.Name, cancellationToken: cancellationToken))
        {
            throw new FluentValidation.ValidationException(
                "A tag with this name already exists.");
        }

        var tag = new Tag(request.Name, request.Color);

        await _commands.AddAsync(tag, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return tag.Id;
    }
}
