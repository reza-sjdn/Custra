using Custra.Application.Common.Authorization;
using Custra.Application.Common.Exceptions;
using Custra.Application.Common.Interfaces;
using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Customers.Commands.CreateCustomer;
using Custra.Application.Customers.Commands.DeleteCustomer;
using Custra.Application.Customers.Commands.UpdateCustomer;
using Custra.Application.Customers.Queries.GetCustomerById;
using Custra.Application.Customers.Queries.GetCustomers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Behaviors;

public sealed class AuthorizationBehavior<TRequest, TResponse>(
    ICurrentUserService currentUserService,
    ICurrentOrganizationService currentOrganizationService,
    IAuthorizationService authorizationService)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ICurrentUserService _currentUserService =
        currentUserService;

    private readonly ICurrentOrganizationService _currentOrganizationService =
        currentOrganizationService;

    private readonly IAuthorizationService _authorizationService =
        authorizationService;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is not IAuthorizationRequest)
            return await next();

        if (!_currentUserService.IsAuthenticated ||
            _currentUserService.UserId is null)
        {
            throw new UnauthorizedAccessException();
        }

        var organizationId =
            await _currentOrganizationService
                .GetOrganizationIdAsync(cancellationToken);

        var permission = GetRequiredPermission(request);

        var hasPermission =
            await _authorizationService.HasPermissionAsync(
                _currentUserService.UserId.Value,
                organizationId,
                permission,
                cancellationToken);

        if (!hasPermission)
        {
            throw new ForbiddenAccessException();
        }

        return await next();
    }

    private static string GetRequiredPermission(TRequest request)
    {
        return request switch
        {
            CreateCustomerCommand =>
                Permissions.Customers.Create,

            GetCustomerByIdQuery =>
                Permissions.Customers.View,

            GetCustomersQuery =>
                Permissions.Customers.View,

            UpdateCustomerCommand =>
                Permissions.Customers.Update,

            DeleteCustomerCommand =>
                Permissions.Customers.Delete,

            _ => throw new InvalidOperationException(
                $"No permission is configured for request type '{typeof(TRequest).Name}'.")
        };
    }
}