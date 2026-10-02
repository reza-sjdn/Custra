using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Models;
using Custra.Application.Contacts.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Contacts.Queries.GetContacts;

public sealed record GetContactsQuery(
    Guid CustomerId,
    string? Search,
    int Page = 1,
    int PageSize = 10)
    : IQuery<PagedResult<ContactDto>>, IAuthorizationRequest;