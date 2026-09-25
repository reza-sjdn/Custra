using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Interfaces.Authorization;

public interface IAuthorizationService
{
    Task<bool> HasPermissionAsync(
        Guid userId,
        Guid organizationId,
        string permission,
        CancellationToken cancellationToken = default);
}