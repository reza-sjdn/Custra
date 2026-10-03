using AutoMapper;
using Custra.Application.Leads.Commands.ChangeLeadStatus;
using Custra.Application.Leads.Commands.CreateLead;
using Custra.Application.Leads.Commands.DeleteLead;
using Custra.Application.Leads.Commands.UpdateLead;
using Custra.Application.Leads.Queries.GetLeadById;
using Custra.Application.Leads.Queries.GetLeads;
using Custra.Domain.Leads;
using Custra.Web.Features.Leads.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Custra.Web.Features.Leads;

public sealed class LeadsController(
    ISender sender,
    IMapper mapper)
    : Controller
{
    private readonly ISender _sender = sender;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<IActionResult> Index(
        string? search,
        LeadStatus? status,
        int page = 1,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(
            new GetLeadsQuery(
                search,
                status,
                page,
                10),
            cancellationToken);

        var viewModel = new LeadListViewModel
        {
            Leads = _mapper.Map<List<LeadViewModel>>(
                result.Items),
            Search = search,
            Status = status,
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
        return View(new CreateLeadViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        CreateLeadViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(model);

        var leadId = await _sender.Send(
            new CreateLeadCommand(
                model.FirstName,
                model.LastName,
                model.CompanyName,
                model.JobTitle,
                model.Email,
                model.Phone),
            cancellationToken);

        return RedirectToAction(
            nameof(Details),
            new { id = leadId });
    }

    [HttpGet]
    public async Task<IActionResult> Details(
        Guid id,
        CancellationToken cancellationToken)
    {
        var leadDto = await _sender.Send(
            new GetLeadByIdQuery(id),
            cancellationToken);

        var viewModel = _mapper.Map<LeadViewModel>(
            leadDto);

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(
        Guid id,
        CancellationToken cancellationToken)
    {
        var leadDto = await _sender.Send(
            new GetLeadByIdQuery(id),
            cancellationToken);

        var viewModel = _mapper.Map<EditLeadViewModel>(
            leadDto);

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        EditLeadViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _sender.Send(
            new UpdateLeadCommand(
                model.Id,
                model.FirstName,
                model.LastName,
                model.CompanyName,
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
        await _sender.Send(
            new DeleteLeadCommand(id),
            cancellationToken);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeStatus(
        Guid id,
        LeadStatus status,
        CancellationToken cancellationToken)
    {
        await _sender.Send(
            new ChangeLeadStatusCommand(id, status),
            cancellationToken);

        return RedirectToAction(
            nameof(Details),
            new { id });
    }
}