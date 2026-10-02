using Custra.Domain.Contacts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Interfaces.Persistence.Commands;

public interface IContactCommands
{
    Task AddContactAsync(
    Contact contact,
    CancellationToken cancellationToken = default);

    Task<Contact?> FindContactAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Contact contact,
        CancellationToken cancellationToken = default);
}
