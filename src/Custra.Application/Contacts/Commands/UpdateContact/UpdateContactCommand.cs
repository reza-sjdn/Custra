using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Contacts.Commands.UpdateContact;

public sealed record UpdateContactCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string? JobTitle,
    string? Email,
    string? Phone)
    : ICommand<Unit>, IAuthorizationRequest;