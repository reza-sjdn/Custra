#region Usings
using Custra.Application.Activities.Commands.CancelActivity;
using Custra.Application.Activities.Commands.CompleteActivity;
using Custra.Application.Activities.Commands.CreateActivity;
using Custra.Application.Activities.Commands.DeleteActivity;
using Custra.Application.Activities.Commands.ReopenActivity;
using Custra.Application.Activities.Commands.UpdateActivity;
using Custra.Application.Activities.Queries.GetActivities;
using Custra.Application.Activities.Queries.GetActivityById;
using Custra.Application.Common.Authorization;
using Custra.Application.Common.Exceptions;
using Custra.Application.Common.Interfaces;
using Custra.Application.Common.Interfaces.Authorization;
using Custra.Application.Contacts.Commands.CreateContact;
using Custra.Application.Contacts.Commands.DeleteContact;
using Custra.Application.Contacts.Commands.UpdateContact;
using Custra.Application.Contacts.Queries.GetContactById;
using Custra.Application.Contacts.Queries.GetContactLookup;
using Custra.Application.Contacts.Queries.GetContacts;
using Custra.Application.Customers.Commands.CreateCustomer;
using Custra.Application.Customers.Commands.DeleteCustomer;
using Custra.Application.Customers.Commands.UpdateCustomer;
using Custra.Application.Customers.Queries.GetCustomerById;
using Custra.Application.Customers.Queries.GetCustomerLookup;
using Custra.Application.Customers.Queries.GetCustomers;
using Custra.Application.Dashboard.Queries.GetDashboard;
using Custra.Application.Leads.Commands.ChangeLeadStatus;
using Custra.Application.Leads.Commands.CreateLead;
using Custra.Application.Leads.Commands.DeleteLead;
using Custra.Application.Leads.Commands.UpdateLead;
using Custra.Application.Leads.Queries.GetLeadById;
using Custra.Application.Leads.Queries.GetLeadLookup;
using Custra.Application.Leads.Queries.GetLeads;
using Custra.Application.Opportunities.Commands.ChangeOpportunityStage;
using Custra.Application.Opportunities.Commands.CreateOpportunity;
using Custra.Application.Opportunities.Commands.DeleteOpportunity;
using Custra.Application.Opportunities.Commands.MarkOpportunityAsLost;
using Custra.Application.Opportunities.Commands.MarkOpportunityAsWon;
using Custra.Application.Opportunities.Commands.ReopenOpportunity;
using Custra.Application.Opportunities.Commands.UpdateOpportunity;
using Custra.Application.Opportunities.Queries.GetOpportunities;
using Custra.Application.Opportunities.Queries.GetOpportunityById;
using Custra.Application.Opportunities.Queries.GetOpportunityLookup;
using Custra.Application.OrganizationUsers.Queries.GetOrganizationUserLookup;
using Custra.Application.SalesPipelines.Commands.CreateSalesPipeline;
using Custra.Application.SalesPipelines.Commands.DeleteSalesPipeline;
using Custra.Application.SalesPipelines.Commands.UpdateSalesPipeline;
using Custra.Application.SalesPipelines.Queries.GetSalesPipelineById;
using Custra.Application.SalesPipelines.Queries.GetSalesPipelineLookup;
using Custra.Application.SalesPipelines.Queries.GetSalesPipelines;
using Custra.Application.SalesPipelineStages.Commands.CreateSalesPipelineStage;
using Custra.Application.SalesPipelineStages.Commands.DeleteSalesPipelineStage;
using Custra.Application.SalesPipelineStages.Commands.UpdateSalesPipelineStage;
using Custra.Application.SalesPipelineStages.Queries.GetSalesPipelineStageById;
using Custra.Application.SalesPipelineStages.Queries.GetSalesPipelineStageLookup;
using Custra.Application.SalesPipelineStages.Queries.GetSalesPipelineStages;
using Custra.Application.Tasks.Commands.CancelTaskItem;
using Custra.Application.Tasks.Commands.CompleteTaskItem;
using Custra.Application.Tasks.Commands.CreateTaskItem;
using Custra.Application.Tasks.Commands.DeleteTaskItem;
using Custra.Application.Tasks.Commands.ReopenTaskItem;
using Custra.Application.Tasks.Commands.StartTaskItem;
using Custra.Application.Tasks.Commands.UpdateTaskItem;
using Custra.Application.Tasks.Queries.GetTaskItemById;
using Custra.Application.Tasks.Queries.GetTaskItemLookup;
using Custra.Application.Tasks.Queries.GetTaskItems;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
#endregion

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

            GetCustomersQuery or
            GetCustomerByIdQuery or
            GetCustomerLookupQuery =>
                Permissions.Customers.View,

            UpdateCustomerCommand =>
                Permissions.Customers.Update,

            DeleteCustomerCommand =>
                Permissions.Customers.Delete,
            #endregion

            #region Contacts

            GetContactsQuery or
            GetContactByIdQuery or
            GetContactLookupQuery =>
                Permissions.Contacts.View,

            CreateContactCommand =>
                Permissions.Contacts.Create,

            UpdateContactCommand =>
                Permissions.Contacts.Update,

            DeleteContactCommand =>
                Permissions.Contacts.Delete,
            #endregion

            #region Leads
            GetLeadsQuery or
            GetLeadByIdQuery or
            GetLeadLookupQuery =>
                Permissions.Leads.View,

            CreateLeadCommand =>
                Permissions.Leads.Create,

            UpdateLeadCommand or
            ChangeLeadStatusCommand =>
                Permissions.Leads.Update,

            DeleteLeadCommand =>
                Permissions.Leads.Delete,
            #endregion

            #region Pipelines and Pipeline Stages
            GetSalesPipelinesQuery or
            GetSalesPipelineByIdQuery or
            GetSalesPipelineStagesQuery or
            GetSalesPipelineStageByIdQuery or
            GetSalesPipelineLookupQuery or
            GetSalesPipelineStageLookupQuery =>
                Permissions.Pipelines.View,

            CreateSalesPipelineCommand or
            CreateSalesPipelineStageCommand =>
                Permissions.Pipelines.Create,

            UpdateSalesPipelineCommand or
            UpdateSalesPipelineStageCommand =>
                Permissions.Pipelines.Update,

            DeleteSalesPipelineCommand or
            DeleteSalesPipelineStageCommand =>
                Permissions.Pipelines.Delete,

            #endregion

            #region Opportunities
            GetOpportunitiesQuery or
            GetOpportunityByIdQuery or
            GetOpportunityLookupQuery =>
                Permissions.Opportunities.View,

            CreateOpportunityCommand =>
                Permissions.Opportunities.Create,

            UpdateOpportunityCommand or
            ChangeOpportunityStageCommand or
            MarkOpportunityAsWonCommand or
            MarkOpportunityAsLostCommand or
            ReopenOpportunityCommand =>
                Permissions.Opportunities.Update,

            DeleteOpportunityCommand =>
                Permissions.Opportunities.Delete,
            #endregion

            #region Organization Users
            GetOrganizationUserLookupQuery =>
                Permissions.Opportunities.View,
            #endregion

            #region Activities
            CreateActivityCommand =>
                Permissions.Activities.Create,

            GetActivitiesQuery or
            GetActivityByIdQuery =>
                Permissions.Activities.View,

            UpdateActivityCommand or
            CompleteActivityCommand or
            CancelActivityCommand or
            ReopenActivityCommand =>
                Permissions.Activities.Update,

            DeleteActivityCommand =>
                Permissions.Activities.Delete,
            #endregion

            #region Tasks
            CreateTaskItemCommand =>
                Permissions.Tasks.Create,

            GetTaskItemsQuery or
            GetTaskItemByIdQuery or
            GetTaskItemLookupQuery =>
                Permissions.Tasks.View,

            UpdateTaskItemCommand or
            StartTaskItemCommand or
            CompleteTaskItemCommand or
            ReopenTaskItemCommand or
            CancelTaskItemCommand =>
                Permissions.Tasks.Update,

            DeleteTaskItemCommand =>
                Permissions.Tasks.Delete,
            #endregion

            #region
            GetDashboardQuery =>
                Permissions.Dashboard.View,
            #endregion

            _ => throw new InvalidOperationException(
                    $"No permission is configured for request type '{typeof(TRequest).Name}'.")
        };
    }
}