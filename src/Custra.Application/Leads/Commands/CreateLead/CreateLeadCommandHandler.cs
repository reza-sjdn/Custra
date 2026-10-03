using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Domain.Leads;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Leads.Commands.CreateLead;

public sealed class CreateLeadCommandHandler(
    ILeadCommands leadCommands,
    IApplicationDbContext dbContext)
    : ICommandHandler<CreateLeadCommand, Guid>
{
    private readonly ILeadCommands _leadCommands = leadCommands;
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Guid> Handle(
        CreateLeadCommand request,
        CancellationToken cancellationToken)
    {
        var lead = new Lead(
            request.FirstName,
            request.LastName,
            request.CompanyName,
            request.JobTitle,
            request.Email,
            request.Phone);

        await _leadCommands.AddAsync(
            lead,
            cancellationToken);

        await _dbContext.SaveChangesAsync(
            cancellationToken);

        return lead.Id;
    }
}