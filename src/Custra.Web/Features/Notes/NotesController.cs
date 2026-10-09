using Custra.Application.Common.Models;
using Custra.Application.Contacts.Queries.GetContactLookup;
using Custra.Application.Customers.Queries.GetCustomerLookup;
using Custra.Application.Leads.Queries.GetLeadLookup;
using Custra.Application.Notes.Commands.CreateNote;
using Custra.Application.Notes.Commands.DeleteNote;
using Custra.Application.Notes.Commands.UpdateNote;
using Custra.Application.Notes.DTOs;
using Custra.Application.Notes.Queries.GetNoteById;
using Custra.Application.Notes.Queries.GetNotes;
using Custra.Application.Opportunities.Queries.GetOpportunityLookup;
using Custra.Web.Features.Notes.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Custra.Web.Features.Notes;


[Authorize]
public sealed class NotesController : Controller
{
    private readonly IMediator _mediator;

    public NotesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index(
        string? search,
        Guid? customerId,
        Guid? contactId,
        Guid? leadId,
        Guid? opportunityId,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(
            new GetNotesQuery(
                search,
                customerId,
                contactId,
                leadId,
                opportunityId,
                page,
                pageSize),
            cancellationToken);

        var model = new NoteListViewModel
        {
            Search = search,
            CustomerId = customerId,
            ContactId = contactId,
            LeadId = leadId,
            OpportunityId = opportunityId,
            Page = result.Page,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount,
            Items = result.Items.Select(MapToViewModel).ToList()
        };

        return View(model);
    }

    public async Task<IActionResult> Details(
        Guid id,
        CancellationToken cancellationToken)
    {
        var note = await _mediator.Send(
            new GetNoteByIdQuery(id),
            cancellationToken);

        if (note is null)
            return NotFound();

        return View(MapToViewModel(note));
    }

    [HttpGet]
    public async Task<IActionResult> Create(
        CancellationToken cancellationToken)
    {
        var model = new NoteFormViewModel();

        await PopulateLookupsAsync(model, cancellationToken);

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        NoteFormViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            await PopulateLookupsAsync(model, cancellationToken);
            return View(model);
        }

        var id = await _mediator.Send(
            new CreateNoteCommand(
                model.Title,
                model.Content,
                model.CustomerId,
                model.ContactId,
                model.LeadId,
                model.OpportunityId),
            cancellationToken);

        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(
        Guid id,
        CancellationToken cancellationToken)
    {
        var note = await _mediator.Send(
            new GetNoteByIdQuery(id),
            cancellationToken);

        if (note is null)
            return NotFound();

        var model = new NoteFormViewModel
        {
            Id = note.Id,
            Title = note.Title,
            Content = note.Content,
            CustomerId = note.CustomerId,
            ContactId = note.ContactId,
            LeadId = note.LeadId,
            OpportunityId = note.OpportunityId
        };

        await PopulateLookupsAsync(model, cancellationToken);

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        NoteFormViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            await PopulateLookupsAsync(model, cancellationToken);
            return View(model);
        }

        await _mediator.Send(
            new UpdateNoteCommand(
                model.Id,
                model.Title,
                model.Content,
                model.CustomerId,
                model.ContactId,
                model.LeadId,
                model.OpportunityId),
            cancellationToken);

        return RedirectToAction(nameof(Details), new { id = model.Id });
    }

    [HttpGet]
    public async Task<IActionResult> DeleteConfirmation(
        Guid id,
        CancellationToken cancellationToken)
    {
        var note = await _mediator.Send(
            new GetNoteByIdQuery(id),
            cancellationToken);

        if (note is null)
            return NotFound();

        return View("Delete", MapToViewModel(note));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new DeleteNoteCommand(id),
            cancellationToken);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> GetContacts(
        Guid customerId,
        CancellationToken cancellationToken)
    {
        if (customerId == Guid.Empty)
            return Json(Array.Empty<object>());

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
        NoteFormViewModel model,
        CancellationToken cancellationToken)
    {
        var customers = await _mediator.Send(
            new GetCustomerLookupQuery(), cancellationToken);

        var leads = await _mediator.Send(
            new GetLeadLookupQuery(), cancellationToken);

        var opportunities = await _mediator.Send(
            new GetOpportunityLookupQuery(), cancellationToken);

        var contacts = model.CustomerId.HasValue
            ? await _mediator.Send(
                new GetContactLookupQuery(model.CustomerId.Value),
                cancellationToken)
            : Array.Empty<LookupItemDto>();

        model.Customers = ToSelectList(customers, model.CustomerId);
        model.Contacts = ToSelectList(contacts, model.ContactId);
        model.Leads = ToSelectList(leads, model.LeadId);
        model.Opportunities = ToSelectList(
            opportunities, model.OpportunityId);
    }

    private static List<SelectListItem> ToSelectList(
        IEnumerable<LookupItemDto> items,
        Guid? selectedId)
    {
        return items.Select(x => new SelectListItem
        {
            Value = x.Id.ToString(),
            Text = x.Name,
            Selected = selectedId == x.Id
        }).ToList();
    }

    private static NoteViewModel MapToViewModel(NoteDto note)
    {
        return new NoteViewModel
        {
            Id = note.Id,
            Title = note.Title,
            Content = note.Content,
            CustomerId = note.CustomerId,
            CustomerName = note.CustomerName,
            ContactId = note.ContactId,
            ContactName = note.ContactName,
            LeadId = note.LeadId,
            LeadName = note.LeadName,
            OpportunityId = note.OpportunityId,
            OpportunityTitle = note.OpportunityTitle,
            CreatedAt = note.CreatedAt,
            ModifiedAt = note.ModifiedAt
        };
    }
}