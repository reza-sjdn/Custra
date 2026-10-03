using Custra.Domain.Leads;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Leads.DTOs;

public sealed record LeadDto(
    Guid Id,
    string FirstName,
    string LastName,
    string? CompanyName,
    string? JobTitle,
    string? Email,
    string? Phone,
    LeadStatus Status,
    DateTime CreatedAt);