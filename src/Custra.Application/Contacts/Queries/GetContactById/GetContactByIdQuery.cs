using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Contacts.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Contacts.Queries.GetContactById;

public sealed record GetContactByIdQuery(Guid Id)
    : IQuery<ContactDto>, IAuthorizationRequest;