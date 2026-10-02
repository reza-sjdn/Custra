using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Contacts.Commands.DeleteContact;

public sealed record DeleteContactCommand(Guid Id)
    : ICommand<Unit>, IAuthorizationRequest;