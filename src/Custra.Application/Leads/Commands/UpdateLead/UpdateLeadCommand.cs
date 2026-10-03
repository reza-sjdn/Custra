using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Leads.Commands.UpdateLead;

public sealed record UpdateLeadCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string? CompanyName,
    string? JobTitle,
    string? Email,
    string? Phone)
    : ICommand<Unit>, IAuthorizationRequest;