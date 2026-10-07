using AutoMapper;
using Custra.Application.Contacts.Queries.GetContactLookup;
using Custra.Application.Customers.Queries.GetCustomerLookup;
using Custra.Application.Opportunities.Commands.ChangeOpportunityStage;
using Custra.Application.Opportunities.Commands.CreateOpportunity;
using Custra.Application.Opportunities.Commands.DeleteOpportunity;
using Custra.Application.Opportunities.Commands.MarkOpportunityAsLost;
using Custra.Application.Opportunities.Commands.MarkOpportunityAsWon;
using Custra.Application.Opportunities.Commands.ReopenOpportunity;
using Custra.Application.Opportunities.Commands.UpdateOpportunity;
using Custra.Application.Opportunities.Queries.GetOpportunities;
using Custra.Application.Opportunities.Queries.GetOpportunityById;
using Custra.Application.OrganizationUsers.Queries.GetOrganizationUserLookup;
using Custra.Application.SalesPipelines.Queries.GetSalesPipelineLookup;
using Custra.Application.SalesPipelineStages.Queries.GetSalesPipelineStageLookup;
using Custra.Domain.Opportunities;
using Custra.Web.Common.ViewModels;
using Custra.Web.Features.Opportunities.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Custra.Web.Features.Opportunities;

public class OpportunitiesController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public OpportunitiesController(
        IMediator mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index(
        string? search,
        OpportunityStatus? status,
        int page = 1)
    {
        var result = await _mediator.Send(
            new GetOpportunitiesQuery(
                Search: search,
                Status: status,
                Page: page));

        var model = new OpportunityListViewModel
        {
            Opportunities = _mapper.Map<IReadOnlyList<OpportunityViewModel>>(result.Items),
            Search = search,
            Status = status,
            Page = result.Page,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount,
            TotalPages = result.TotalPages
        };

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        var model = new CreateOpportunityViewModel();

        await PopulateCreateLookupsAsync(model, cancellationToken);

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        CreateOpportunityViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            await PopulateCreateLookupsAsync(model, cancellationToken);
            return View(model);
        }


        var id = await _mediator.Send(
            new CreateOpportunityCommand(
                model.CustomerId,
                model.ContactId,
                model.OwnerUserId,
                model.SalesPipelineId,
                model.SalesPipelineStageId,
                model.Title,
                model.Description,
                model.EstimatedValue,
                model.ExpectedCloseDate));

        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var opportunity = await _mediator.Send(
            new GetOpportunityByIdQuery(id));

        var model = _mapper.Map<OpportunityViewModel>(opportunity);

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id,
        CancellationToken cancellationToken)
    {
        var opportunity = await _mediator.Send(
            new GetOpportunityByIdQuery(id));

        var model = _mapper.Map<EditOpportunityViewModel>(opportunity);

        await PopulateEditLookupsAsync(model, cancellationToken);

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        EditOpportunityViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            await PopulateEditLookupsAsync(model, cancellationToken);
            return View(model);
        }

        await _mediator.Send(
            new UpdateOpportunityCommand(
                model.Id,
                model.CustomerId,
                model.ContactId,
                model.OwnerUserId,
                model.SalesPipelineId,
                model.SalesPipelineStageId,
                model.Title,
                model.Description,
                model.EstimatedValue,
                model.ExpectedCloseDate));

        return RedirectToAction(nameof(Details), new { id = model.Id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(
            new DeleteOpportunityCommand(id));

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeStage(
        Guid id,
        Guid salesPipelineStageId)
    {
        await _mediator.Send(
            new ChangeOpportunityStageCommand(
                id,
                salesPipelineStageId));

        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarkWon(Guid id)
    {
        await _mediator.Send(
            new MarkOpportunityAsWonCommand(id));

        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarkLost(Guid id)
    {
        await _mediator.Send(
            new MarkOpportunityAsLostCommand(id));

        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reopen(Guid id)
    {
        await _mediator.Send(
            new ReopenOpportunityCommand(id));

        return RedirectToAction(nameof(Details), new { id });
    }

    private async Task PopulateCreateLookupsAsync(
        CreateOpportunityViewModel model,
        CancellationToken cancellationToken)
    {
        var customers = await _mediator.Send(
            new GetCustomerLookupQuery(),
            cancellationToken);

        var owners = await _mediator.Send(
            new GetOrganizationUserLookupQuery(),
            cancellationToken);

        var pipelines = await _mediator.Send(
            new GetSalesPipelineLookupQuery(),
            cancellationToken);

        model.Customers = customers
            .Select(x => new LookupItemViewModel
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToList();

        model.Owners = owners
            .Select(x => new LookupItemViewModel
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToList();

        model.Pipelines = pipelines
            .Select(x => new LookupItemViewModel
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToList();

        if (model.CustomerId != Guid.Empty)
        {
            var contacts = await _mediator.Send(
                new GetContactLookupQuery(model.CustomerId),
                cancellationToken);

            model.Contacts = contacts
                .Select(x => new LookupItemViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();
        }

        if (model.SalesPipelineId != Guid.Empty)
        {
            var stages = await _mediator.Send(
                new GetSalesPipelineStageLookupQuery(model.SalesPipelineId),
                cancellationToken);

            model.Stages = stages
                .Select(x => new LookupItemViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();
        }
    }

    private async Task PopulateEditLookupsAsync(
        EditOpportunityViewModel model,
        CancellationToken cancellationToken)
    {
        var customers = await _mediator.Send(
            new GetCustomerLookupQuery(),
            cancellationToken);

        var owners = await _mediator.Send(
            new GetOrganizationUserLookupQuery(),
            cancellationToken);

        var pipelines = await _mediator.Send(
            new GetSalesPipelineLookupQuery(),
            cancellationToken);

        model.Customers = customers
            .Select(x => new LookupItemViewModel
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToList();

        model.Owners = owners
            .Select(x => new LookupItemViewModel
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToList();

        model.Pipelines = pipelines
            .Select(x => new LookupItemViewModel
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToList();

        if (model.CustomerId != Guid.Empty)
        {
            var contacts = await _mediator.Send(
                new GetContactLookupQuery(model.CustomerId),
                cancellationToken);

            model.Contacts = contacts
                .Select(x => new LookupItemViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();
        }

        if (model.SalesPipelineId != Guid.Empty)
        {
            var stages = await _mediator.Send(
                new GetSalesPipelineStageLookupQuery(model.SalesPipelineId),
                cancellationToken);

            model.Stages = stages
                .Select(x => new LookupItemViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();
        }
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

    [HttpGet]
    public async Task<IActionResult> GetStages(
        Guid pipelineId,
        CancellationToken cancellationToken)
    {
        var stages = await _mediator.Send(
            new GetSalesPipelineStageLookupQuery(pipelineId),
            cancellationToken);

        return Json(stages);
    }

}