using AutoMapper;
using Custra.Application.Contacts.Commands.CreateContact;
using Custra.Application.Contacts.Commands.DeleteContact;
using Custra.Application.Contacts.Commands.UpdateContact;
using Custra.Application.Contacts.Queries.GetContactById;
using Custra.Application.Contacts.Queries.GetContacts;
using Custra.Application.Tags.Queries.GetTagLookup;
using Custra.Web.Features.Contacts.ViewModels;
using Custra.Web.Features.Tags.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Custra.Web.Features.Contacts;

public sealed class ContactsController(
    ISender sender,
    IMapper mapper,
    ITagAssignmentViewModelService tagAssignmentViewModelService)
    : Controller
{
    private readonly ISender _sender = sender;
    private readonly IMapper _mapper = mapper;
    private readonly ITagAssignmentViewModelService _tagAssignmentViewModelService = tagAssignmentViewModelService;

    [HttpGet]
    public async Task<IActionResult> Index(
        Guid customerId,
        string? search,
        Guid? tagId,
        int page = 1,
        CancellationToken cancellationToken = default)
    {
        var tags = await _sender.Send(new GetTagLookupQuery());

        var result = await _sender.Send(
            new GetContactsQuery(
                customerId,
                search,
                tagId,
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

        viewModel.TagId = tagId;
        viewModel.AvailableTags = tags
            .Select(tag => new SelectListItem(tag.Name, tag.Id.ToString()))
            .ToList();

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

        viewModel.TagAssignments = await _tagAssignmentViewModelService
            .PopulateAsync("Contact", id);

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