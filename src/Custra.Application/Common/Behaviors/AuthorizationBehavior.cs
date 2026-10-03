using Custra.Application.Common.Authorization;
using Custra.Application.Common.Exceptions;
using Custra.Application.Common.Interfaces;
using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Contacts.Commands.CreateContact;
using Custra.Application.Contacts.Commands.DeleteContact;
using Custra.Application.Contacts.Commands.UpdateContact;
using Custra.Application.Contacts.Queries.GetContactById;
using Custra.Application.Contacts.Queries.GetContacts;
using Custra.Application.Customers.Commands.CreateCustomer;
using Custra.Application.Customers.Commands.DeleteCustomer;
using Custra.Application.Customers.Commands.UpdateCustomer;
using Custra.Application.Customers.Queries.GetCustomerById;
using Custra.Application.Customers.Queries.GetCustomers;
using Custra.Application.Leads.Commands.ChangeLeadStatus;
using Custra.Application.Leads.Commands.CreateLead;
using Custra.Application.Leads.Commands.DeleteLead;
using Custra.Application.Leads.Commands.UpdateLead;
using Custra.Application.Leads.Queries.GetLeadById;
using Custra.Application.Leads.Queries.GetLeads;
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
            #region Customers
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
            #endregion

            #region Contacts
            GetContactByIdQuery =>
                Permissions.Contacts.View,

            GetContactsQuery =>
                Permissions.Contacts.View,

            CreateContactCommand =>
                Permissions.Contacts.Create,

            UpdateContactCommand =>
                Permissions.Contacts.Update,

            DeleteContactCommand =>
                Permissions.Contacts.Delete,
            #endregion

            #region Leads
            GetLeadByIdQuery =>
                Permissions.Leads.View,

            GetLeadsQuery =>
                Permissions.Leads.View,

            CreateLeadCommand =>
                Permissions.Leads.Create,

            UpdateLeadCommand =>
                Permissions.Leads.Update,

            DeleteLeadCommand =>
                Permissions.Leads.Delete,

            ChangeLeadStatusCommand =>
                Permissions.Leads.Update,
            #endregion

            _ => throw new InvalidOperationException(
                $"No permission is configured for request type '{typeof(TRequest).Name}'.")
        };
    }
}