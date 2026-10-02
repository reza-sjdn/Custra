using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Contacts.Commands.CreateContact;

public sealed record CreateContactCommand(
    Guid CustomerId,
    string FirstName,
    string LastName,
    string? JobTitle,
    string? Email,
    string? Phone)
    : ICommand<Guid>, IAuthorizationRequest;