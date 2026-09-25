using AutoMapper;
using Custra.Application.Customers.Commands.CreateCustomer;
using Custra.Application.Customers.Commands.DeleteCustomer;
using Custra.Application.Customers.Commands.UpdateCustomer;
using Custra.Application.Customers.Queries.GetCustomerById;
using Custra.Application.Customers.Queries.GetCustomers;
using Custra.Web.Features.Customers.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Custra.Web.Features.Customers;

public sealed class CustomersController : Controller
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public CustomersController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index(
        string? search,
        int page = 1,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(
            new GetCustomersQuery(search, page, 2),
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
