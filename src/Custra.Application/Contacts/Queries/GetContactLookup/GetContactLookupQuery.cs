using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Contacts.Queries.GetContactLookup;

public sealed record GetContactLookupQuery(
    Guid CustomerId)
    : IQuery<IReadOnlyList<LookupItemDto>>,
      IAuthorizationRequest;