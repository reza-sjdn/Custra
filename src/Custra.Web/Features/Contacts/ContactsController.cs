using AutoMapper;
using Custra.Application.Contacts.Commands.CreateContact;
using Custra.Application.Contacts.Commands.DeleteContact;
using Custra.Application.Contacts.Commands.UpdateContact;
using Custra.Application.Contacts.Queries.GetContactById;
using Custra.Application.Contacts.Queries.GetContacts;
using Custra.Web.Features.Contacts.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Custra.Web.Features.Contacts;

public sealed class ContactsController(
    ISender sender,
    IMapper mapper)
    : Controller
{
    private readonly ISender _sender = sender;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<IActionResult> Index(
        Guid customerId,
        string? search,
        int page = 1,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(
            new GetContactsQuery(
                customerId,
                search,
                page,
                10),
            cancellationToken);

        var viewModel = new ContactListViewModel
        {
            CustomerId = customerId,
            Contacts = _mapper.Map<List<ContactViewModel>>(
                result.Items),
            Search = search,
            Page = result.Page,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount,
            TotalPages = result.TotalPages
        };

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Create(Guid customerId)
    {
        return View(new CreateContactViewModel
        {
            CustomerId = customerId
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        CreateContactViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(model);

        var contactId = await _sender.Send(
            new CreateContactCommand(
                model.CustomerId,
                model.FirstName,
                model.LastName,
                model.JobTitle,
                model.Email,
                model.Phone),
            cancellationToken);

        return RedirectToAction(
            nameof(Details),
            new { id = contactId });
    }

    [HttpGet]
    public async Task<IActionResult> Details(
        Guid id,
        CancellationToken cancellationToken)
    {
        var contactDto = await _sender.Send(
            new GetContactByIdQuery(id),
            cancellationToken);

        var viewModel = _mapper.Map<ContactViewModel>(
            contactDto);

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(
        Guid id,
        CancellationToken cancellationToken)
    {
        var contactDto = await _sender.Send(
            new GetContactByIdQuery(id),
            cancellationToken);

        var viewModel = _mapper.Map<EditContactViewModel>(
            contactDto);

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        EditContactViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _sender.Send(
            new UpdateContactCommand(
                model.Id,
                model.FirstName,
                model.LastName,
                model.JobTitle,
                model.Email,
                model.Phone),
            cancellationToken);

        return RedirectToAction(
            nameof(Details),
            new { id = model.Id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        var contact = await _sender.Send(
            new GetContactByIdQuery(id),
            cancellationToken);

        await _sender.Send(
            new DeleteContactCommand(id),
            cancellationToken);

        return RedirectToAction(
            nameof(Index),
            new { customerId = contact.CustomerId });
    }
}