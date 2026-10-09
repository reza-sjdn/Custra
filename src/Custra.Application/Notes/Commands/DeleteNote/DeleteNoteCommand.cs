using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Notes.Commands.DeleteNote;


public sealed record DeleteNoteCommand(Guid Id)
    : IRequest<Unit>, IAuthorizationRequest
{
}

public sealed class DeleteNoteCommandValidator
    : AbstractValidator<DeleteNoteCommand>
{
    public DeleteNoteCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

public sealed class DeleteNoteCommandHandler
    : IRequestHandler<DeleteNoteCommand, Unit>
{
    private readonly INoteCommands _commands;
    private readonly IApplicationDbContext _dbContext;

    public DeleteNoteCommandHandler(
        INoteCommands commands,
        IApplicationDbContext dbContext)
    {
        _commands = commands;
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(
        DeleteNoteCommand request,
        CancellationToken cancellationToken)
    {
        var note = await _commands.FindAsync(
            request.Id, cancellationToken);

        if (note is null)
            throw new KeyNotFoundException("Note not found.");

        _commands.Delete(note);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}