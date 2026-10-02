using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Contacts.DTOs;

public sealed record ContactDto(
    Guid Id,
    Guid CustomerId,
    string FirstName,
    string LastName,
    string? JobTitle,
    string? Email,
    string? Phone);