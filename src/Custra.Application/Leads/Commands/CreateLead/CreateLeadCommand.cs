using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Leads.Commands.CreateLead;

public sealed record CreateLeadCommand(
    string FirstName,
    string LastName,
    string? CompanyName,
    string? JobTitle,
    string? Email,
    string? Phone)
    : ICommand<Guid>, IAuthorizationRequest;