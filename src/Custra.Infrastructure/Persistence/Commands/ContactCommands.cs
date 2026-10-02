using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Domain.Contacts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Infrastructure.Persistence.Commands;

public sealed class ContactCommands(CustraDbContext dbContext)
    : IContactCommands
{
    private readonly CustraDbContext _dbContext = dbContext;

    public async Task AddContactAsync(
    Contact contact,
    CancellationToken cancellationToken = default)
    {
        await _dbContext.Contacts.AddAsync(contact, cancellationToken);
    }

    public async Task<Contact?> FindContactAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Contacts
            .SingleOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public Task DeleteAsync(
        Contact contact,
        CancellationToken cancellationToken = default)
    {
        _dbContext.Contacts.Remove(contact);
        return Task.CompletedTask;
    }
}
