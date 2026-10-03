using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using Custra.Domain.Leads;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Leads.Commands.ChangeLeadStatus;

public sealed record ChangeLeadStatusCommand(
    Guid Id,
    LeadStatus Status)
    : ICommand<Unit>, IAuthorizationRequest;