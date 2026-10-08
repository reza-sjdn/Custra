using AutoMapper;
using Custra.Application.Activities.Commands.CancelActivity;
using Custra.Application.Activities.Commands.CompleteActivity;
using Custra.Application.Activities.Commands.CreateActivity;
using Custra.Application.Activities.Commands.DeleteActivity;
using Custra.Application.Activities.Commands.ReopenActivity;
using Custra.Application.Activities.Commands.UpdateActivity;
using Custra.Application.Activities.Queries.GetActivities;
using Custra.Application.Activities.Queries.GetActivityById;
using Custra.Application.Common.Models;
using Custra.Application.Contacts.Queries.GetContactLookup;
using Custra.Application.Customers.Queries.GetCustomerLookup;
using Custra.Application.Leads.Queries.GetLeadLookup;
using Custra.Application.Opportunities.Queries.GetOpportunityLookup;
using Custra.Application.OrganizationUsers.Queries.GetOrganizationUserLookup;
using Custra.Domain.Activities;
using Custra.Web.Common.ViewModels;
using Custra.Web.Features.Activities.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Custra.Web.Features.Activities;

public sealed class ActivitiesController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ActivitiesController(
        IMediator mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index(
        string? search,
        ActivityStatus? status,
        ActivityType? type,
        Guid? ownerUserId,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(
            new GetActivitiesQuery(
                Search: search,
                Status: status,
                Type: type,
                OwnerUserId: ownerUserId,
                Page: page,
                PageSize: pageSize),
            cancellationToken);

        var model = new ActivityListViewModel
        {
            Activities = _mapper.Map<IReadOnlyList<ActivityViewModel>>(
                result.Items),

            Search = search,
            Status = status,
            Type = type,
            OwnerUserId = ownerUserId,

            Page = result.Page,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount
        };

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Details(
        Guid id,
        CancellationToken cancellationToken)
    {
        var activity = await _mediator.Send(
            new GetActivityByIdQuery(id),
            cancellationToken);

        if (activity is null)
            return NotFound();

        return View(
            _mapper.Map<ActivityViewModel>(activity));
    }

    [HttpGet]
    public async Task<IActionResult> Create(
        CancellationToken cancellationToken)
    {
        var model = new CreateActivityViewModel();

        await PopulateLookupsAsync(
            model,
            cancellationToken);

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        CreateActivityViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            await PopulateLookupsAsync(
                model,
                cancellationToken);

            return View(model);
        }

        var id = await _mediator.Send(
            new CreateActivityCommand(
                model.Subject,
                model.Description,
                model.Type,
                model.OwnerUserId,
                model.DueDate,
                model.CustomerId,
                model.ContactId,
                model.LeadId,
                model.OpportunityId),
            cancellationToken);

        return RedirectToAction(
            nameof(Details),
            new { id });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(
        Guid id,
        CancellationToken cancellationToken)
    {
        var activity = await _mediator.Send(
            new GetActivityByIdQuery(id),
            cancellationToken);

        if (activity is null)
            return NotFound();

        var model = _mapper.Map<EditActivityViewModel>(activity);

        await PopulateLookupsAsync(
            model,
            cancellationToken);

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        EditActivityViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            await PopulateLookupsAsync(
                model,
                cancellationToken);

            return View(model);
        }

        await _mediator.Send(
            new UpdateActivityCommand(
                model.Id,
                model.Subject,
                model.Description,
                model.Type,
                model.OwnerUserId,
                model.DueDate,
                model.CustomerId,
                model.ContactId,
                model.LeadId,
                model.OpportunityId),
            cancellationToken);

        return RedirectToAction(
            nameof(Details),
            new { id = model.Id });
    }

    [HttpGet]
    public async Task<IActionResult> DeleteConfirmation(
        Guid id,
        CancellationToken cancellationToken)
    {
        var activity = await _mediator.Send(
            new GetActivityByIdQuery(id),
            cancellationToken);

        if (activity is null)
            return NotFound();

        return View(_mapper.Map<ActivityViewModel>(activity));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new DeleteActivityCommand(id),
            cancellationToken);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Complete(
        Guid id,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new CompleteActivityCommand(id),
            cancellationToken);

        return RedirectToAction(
            nameof(Details),
            new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(
        Guid id,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new CancelActivityCommand(id),
            cancellationToken);

        return RedirectToAction(
            nameof(Details),
            new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reopen(
        Guid id,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new ReopenActivityCommand(id),
            cancellationToken);

        return RedirectToAction(
            nameof(Details),
            new { id });
    }

    private async Task PopulateLookupsAsync(
        CreateActivityViewModel model,
        CancellationToken cancellationToken)
    {
        var owners = await _mediator.Send(
            new GetOrganizationUserLookupQuery(),
            cancellationToken);

        var customers = await _mediator.Send(
            new GetCustomerLookupQuery(),
            cancellationToken);

        model.Owners = MapLookupItems(owners);
        model.Customers = MapLookupItems(customers);

        if (model.CustomerId.HasValue)
        {
            var contacts = await _mediator.Send(
                new GetContactLookupQuery(model.CustomerId.Value),
                cancellationToken);

            model.Contacts = MapLookupItems(contacts);
        }

        var leads = await _mediator.Send(
            new GetLeadLookupQuery(),
            cancellationToken);

        var opportunities = await _mediator.Send(
            new GetOpportunityLookupQuery(),
            cancellationToken);

        model.Leads = MapLookupItems(leads);
        model.Opportunities = MapLookupItems(opportunities);
    }

    private async Task PopulateLookupsAsync(
        EditActivityViewModel model,
        CancellationToken cancellationToken)
    {
        var owners = await _mediator.Send(
            new GetOrganizationUserLookupQuery(),
            cancellationToken);

        var customers = await _mediator.Send(
            new GetCustomerLookupQuery(),
            cancellationToken);

        model.Owners = MapLookupItems(owners);
        model.Customers = MapLookupItems(customers);

        if (model.CustomerId.HasValue)
        {
            var contacts = await _mediator.Send(
                new GetContactLookupQuery(model.CustomerId.Value),
                cancellationToken);

            model.Contacts = MapLookupItems(contacts);
        }

        var leads = await _mediator.Send(
            new GetLeadLookupQuery(),
            cancellationToken);

        var opportunities = await _mediator.Send(
            new GetOpportunityLookupQuery(),
            cancellationToken);

        model.Leads = MapLookupItems(leads);
        model.Opportunities = MapLookupItems(opportunities);
    }

    private static IReadOnlyList<LookupItemViewModel> MapLookupItems(
        IReadOnlyList<LookupItemDto> items)
    {
        return items
            .Select(x => new LookupItemViewModel
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToList();
    }

    [HttpGet]
    public async Task<IActionResult> GetContacts(
        Guid customerId,
        CancellationToken cancellationToken)
    {
        var contacts = await _mediator.Send(
            new GetContactLookupQuery(customerId),
            cancellationToken);

        return Json(contacts);
    }

}