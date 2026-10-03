using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Leads.Commands.DeleteLead;

public sealed record DeleteLeadCommand(Guid Id)
    : ICommand<Unit>, IAuthorizationRequest;