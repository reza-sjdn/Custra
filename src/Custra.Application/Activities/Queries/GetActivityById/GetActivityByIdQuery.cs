using Custra.Application.Activities.DTOs;
using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Common.Interfaces.CQRS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Activities.Queries.GetActivityById;

public sealed record GetActivityByIdQuery(Guid Id)
    : IQuery<ActivityDto?>,
      IAuthorizationRequest;