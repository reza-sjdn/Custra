using AutoMapper;
using Custra.Application.Customers.Commands.CreateCustomer;
using Custra.Application.Customers.Commands.DeleteCustomer;
using Custra.Application.Customers.Commands.UpdateCustomer;
using Custra.Application.Customers.Queries.GetCustomerById;
using Custra.Application.Customers.Queries.GetCustomers;
using Custra.Application.Tags.Queries.GetTagLookup;
using Custra.Web.Features.Customers.ViewModels;
using Custra.Web.Features.Tags.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Custra.Web.Features.Customers;

public sealed class CustomersController : Controller
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly ITagAssignmentViewModelService _tagAssignmentViewModelService;

    public CustomersController(ISender sender, IMapper mapper,
                               ITagAssignmentViewModelService tagAssignmentViewModelService)
    {
        _sender = sender;
        _mapper = mapper;
        _tagAssignmentViewModelService = tagAssignmentViewModelService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(
        string? search,
        Guid? tagId,
        int page = 1,
        CancellationToken cancellationToken = default)
    {
        var tags = await _sender.Send(new GetTagLookupQuery());

        var result = await _sender.Send(
            new GetCustomersQuery(search, tagId, page, 3),
            cancellationToken);

        var viewModel = new CustomerListViewModel
        {
            Customers = _mapper.Map<List<CustomerViewModel>>(result.Items),
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
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        CreateCustomerViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(model);

        var command = new CreateCustomerCommand(model.Name);

        var customerId = await _sender.Send(command, cancellationToken);

        return RedirectToAction(nameof(Details), new { id = customerId });
    }

    [HttpGet]
    public async Task<IActionResult> Details(
        Guid id,
        CancellationToken cancellationToken)
    {
        var customerDto = await _sender.Send(
            new GetCustomerByIdQuery(id),
            cancellationToken);

        var customerViewModel = _mapper.Map<CustomerViewModel>(customerDto);

        customerViewModel.TagAssignments = await _tagAssignmentViewModelService
            .PopulateAsync("Customer", id);

        return View(customerViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(
    Guid id,
    CancellationToken cancellationToken)
    {
        var customerDto = await _sender.Send(
            new GetCustomerByIdQuery(id),
            cancellationToken);

        var viewModel = _mapper.Map<EditCustomerViewModel>(
            customerDto);

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        EditCustomerViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _sender.Send(
            new UpdateCustomerCommand(
                model.Id,
                model.Name),
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
        await _sender.Send(
            new DeleteCustomerCommand(id),
            cancellationToken);

        return RedirectToAction(nameof(Index));
    }

}
