using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Leads.Commands.UpdateLead;

public sealed class UpdateLeadCommandHandler(
    ILeadCommands leadCommands,
    IApplicationDbContext dbContext)
    : ICommandHandler<UpdateLeadCommand, Unit>
{
    private readonly ILeadCommands _leadCommands = leadCommands;
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Unit> Handle(
        UpdateLeadCommand request,
        CancellationToken cancellationToken)
    {
        var lead = await _leadCommands.FindAsync(
            request.Id,
            cancellationToken);

        if (lead is null)
            throw new KeyNotFoundException(
                "Lead was not found.");

        lead.Update(
            request.FirstName,
            request.LastName,
            request.CompanyName,
            request.JobTitle,
            request.Email,
            request.Phone);

        await _dbContext.SaveChangesAsync(
            cancellationToken);

        return Unit.Value;
    }
}
