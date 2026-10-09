using Custra.Application.Common.Models;
using Custra.Application.Contacts.Queries.GetContactLookup;
using Custra.Application.Customers.Queries.GetCustomerLookup;
using Custra.Application.Leads.Queries.GetLeadLookup;
using Custra.Application.Opportunities.Queries.GetOpportunityLookup;
using Custra.Application.OrganizationUsers.Queries.GetOrganizationUserLookup;
using Custra.Application.Tasks.Commands.CancelTaskItem;
using Custra.Application.Tasks.Commands.CompleteTaskItem;
using Custra.Application.Tasks.Commands.CreateTaskItem;
using Custra.Application.Tasks.Commands.DeleteTaskItem;
using Custra.Application.Tasks.Commands.ReopenTaskItem;
using Custra.Application.Tasks.Commands.StartTaskItem;
using Custra.Application.Tasks.Commands.UpdateTaskItem;
using Custra.Application.Tasks.DTOs;
using Custra.Application.Tasks.Queries.GetTaskItemById;
using Custra.Application.Tasks.Queries.GetTaskItems;
using Custra.Domain.Tasks;
using Custra.Web.Features.Tasks.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Custra.Web.Features.Tasks;

[Authorize]
public sealed class TasksController : Controller
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index(
        string? search,
        TaskItemStatus? status,
        TaskItemPriority? priority,
        Guid? ownerUserId,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(
            new GetTaskItemsQuery(
                Search: search,
                Status: status,
                Priority: priority,
                OwnerUserId: ownerUserId,
                Page: page,
                PageSize: pageSize),
            cancellationToken);

        var model = new TaskItemListViewModel
        {
            Items = result.Items.Select(MapToViewModel).ToList(),
            Search = search,
            Status = status,
            Priority = priority,
            OwnerUserId = ownerUserId,
            Page = result.Page,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount
        };

        return View(model);
    }

    public async Task<IActionResult> Details(
        Guid id,
        CancellationToken cancellationToken)
    {
        var task = await _mediator.Send(
            new GetTaskItemByIdQuery(id),
            cancellationToken);

        if (task is null)
        {
            return NotFound();
        }

        return View(MapToViewModel(task));
    }

    [HttpGet]
    public async Task<IActionResult> Create(
        CancellationToken cancellationToken)
    {
        var model = new CreateTaskItemViewModel();

        await PopulateLookupsAsync(model, cancellationToken);

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        CreateTaskItemViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            await PopulateLookupsAsync(model, cancellationToken);
            return View(model);
        }

        var id = await _mediator.Send(
            new CreateTaskItemCommand(
                model.Title,
                model.Description,
                model.Priority,
                model.DueDate,
                model.OwnerUserId,
                model.CustomerId,
                model.ContactId,
                model.LeadId,
                model.OpportunityId,
                ActivityId: null),
            cancellationToken);

        TempData["SuccessMessage"] = "Task created successfully.";

        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(
        Guid id,
        CancellationToken cancellationToken)
    {
        var task = await _mediator.Send(
            new GetTaskItemByIdQuery(id),
            cancellationToken);

        if (task is null)
        {
            return NotFound();
        }

        var model = new EditTaskItemViewModel
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Priority = task.Priority,
            DueDate = task.DueDate,
            OwnerUserId = task.OwnerUserId,
            CustomerId = task.CustomerId,
            ContactId = task.ContactId,
            LeadId = task.LeadId,
            OpportunityId = task.OpportunityId,
            ActivityId = task.ActivityId
        };

        await PopulateLookupsAsync(
            model,
            cancellationToken,
            task.ContactId);

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        EditTaskItemViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            await PopulateLookupsAsync(
                model,
                cancellationToken,
                model.ContactId);

            return View(model);
        }

        await _mediator.Send(
            new UpdateTaskItemCommand(
                model.Id,
                model.Title,
                model.Description,
                model.Priority,
                model.DueDate,
                model.OwnerUserId,
                model.CustomerId,
                model.ContactId,
                model.LeadId,
                model.OpportunityId,
                model.ActivityId),
            cancellationToken);

        TempData["SuccessMessage"] = "Task updated successfully.";

        return RedirectToAction(nameof(Details), new { id = model.Id });
    }

    [HttpGet]
    public async Task<IActionResult> DeleteConfirmation(
        Guid id,
        CancellationToken cancellationToken)
    {
        var task = await _mediator.Send(
            new GetTaskItemByIdQuery(id),
            cancellationToken);

        if (task is null)
        {
            return NotFound();
        }

        return View("Delete", MapToViewModel(task));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new DeleteTaskItemCommand(id),
            cancellationToken);

        TempData["SuccessMessage"] = "Task deleted successfully.";

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Start(
        Guid id,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new StartTaskItemCommand(id), cancellationToken);
        TempData["SuccessMessage"] = "Task started.";

        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Complete(
        Guid id,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new CompleteTaskItemCommand(id), cancellationToken);
        TempData["SuccessMessage"] = "Task completed.";

        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(
        Guid id,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new CancelTaskItemCommand(id), cancellationToken);
        TempData["SuccessMessage"] = "Task cancelled.";

        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reopen(
        Guid id,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new ReopenTaskItemCommand(id), cancellationToken);
        TempData["SuccessMessage"] = "Task reopened.";

        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpGet]
    public async Task<IActionResult> GetContacts(
        Guid customerId,
        CancellationToken cancellationToken)
    {
        if (customerId == Guid.Empty)
        {
            return Json(Array.Empty<object>());
        }

        var contacts = await _mediator.Send(
            new GetContactLookupQuery(customerId),
            cancellationToken);

        return Json(contacts.Select(x => new
        {
            id = x.Id,
            name = x.Name
        }));
    }

    private async Task PopulateLookupsAsync(
        CreateTaskItemViewModel model,
        CancellationToken cancellationToken)
    {
        var owners = await _mediator.Send(
            new GetOrganizationUserLookupQuery(),
            cancellationToken);

        var customers = await _mediator.Send(
            new GetCustomerLookupQuery(),
            cancellationToken);

        var leads = await _mediator.Send(
            new GetLeadLookupQuery(),
            cancellationToken);

        var opportunities = await _mediator.Send(
            new GetOpportunityLookupQuery(),
            cancellationToken);

        var contacts = model.CustomerId.HasValue
            ? await _mediator.Send(
                new GetContactLookupQuery(model.CustomerId.Value),
                cancellationToken)
            : Array.Empty<LookupItemDto>();

        model.Owners = ToSelectList(owners, model.OwnerUserId);
        model.Customers = ToSelectList(customers, model.CustomerId);
        model.Contacts = ToSelectList(contacts, model.ContactId);
        model.Leads = ToSelectList(leads, model.LeadId);
        model.Opportunities = ToSelectList(opportunities, model.OpportunityId);
    }

    private async Task PopulateLookupsAsync(
        EditTaskItemViewModel model,
        CancellationToken cancellationToken,
        Guid? selectedContactId = null)
    {
        var owners = await _mediator.Send(
            new GetOrganizationUserLookupQuery(),
            cancellationToken);

        var customers = await _mediator.Send(
            new GetCustomerLookupQuery(),
            cancellationToken);

        var leads = await _mediator.Send(
            new GetLeadLookupQuery(),
            cancellationToken);

        var opportunities = await _mediator.Send(
            new GetOpportunityLookupQuery(),
            cancellationToken);

        var contacts = model.CustomerId.HasValue
            ? await _mediator.Send(
                new GetContactLookupQuery(model.CustomerId.Value),
                cancellationToken)
            : Array.Empty<LookupItemDto>();

        model.Owners = ToSelectList(owners, model.OwnerUserId);
        model.Customers = ToSelectList(customers, model.CustomerId);
        model.Contacts = ToSelectList(contacts, selectedContactId);
        model.Leads = ToSelectList(leads, model.LeadId);
        model.Opportunities = ToSelectList(opportunities, model.OpportunityId);
    }

    private static List<SelectListItem> ToSelectList(
        IEnumerable<LookupItemDto> items,
        Guid? selectedId)
    {
        return items.Select(x => new SelectListItem
        {
            Value = x.Id.ToString(),
            Text = x.Name,
            Selected = selectedId.HasValue && x.Id == selectedId.Value
        }).ToList();
    }

    private static TaskItemViewModel MapToViewModel(TaskItemDto task)
    {
        return new TaskItemViewModel
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
            Priority = task.Priority,
            DueDate = task.DueDate,
            CompletedAt = task.CompletedAt,
            OwnerUserId = task.OwnerUserId,
            OwnerName = task.OwnerName,
            CustomerId = task.CustomerId,
            CustomerName = task.CustomerName,
            ContactId = task.ContactId,
            ContactName = task.ContactName,
            LeadId = task.LeadId,
            LeadName = task.LeadName,
            OpportunityId = task.OpportunityId,
            OpportunityTitle = task.OpportunityTitle,
            ActivityId = task.ActivityId,
            ActivitySubject = task.ActivitySubject,
            CreatedAt = task.CreatedAt
        };
    }
}