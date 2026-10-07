using AutoMapper;
using Custra.Application.SalesPipelines.Commands.CreateSalesPipeline;
using Custra.Application.SalesPipelines.Commands.DeleteSalesPipeline;
using Custra.Application.SalesPipelines.Commands.UpdateSalesPipeline;
using Custra.Application.SalesPipelines.Queries.GetSalesPipelineById;
using Custra.Application.SalesPipelines.Queries.GetSalesPipelines;
using Custra.Application.SalesPipelineStages.Commands.CreateSalesPipelineStage;
using Custra.Application.SalesPipelineStages.Commands.DeleteSalesPipelineStage;
using Custra.Application.SalesPipelineStages.Commands.UpdateSalesPipelineStage;
using Custra.Application.SalesPipelineStages.Queries.GetSalesPipelineStageById;
using Custra.Web.Features.SalesPipelines.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Custra.Web.Features.SalesPipelines;

public class SalesPipelinesController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public SalesPipelinesController(
        IMediator mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var pipelines = await _mediator.Send(
            new GetSalesPipelinesQuery());

        var model = _mapper.Map<IReadOnlyList<SalesPipelineViewModel>>(
            pipelines);

        return View(model);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var pipeline = await _mediator.Send(
            new GetSalesPipelineByIdQuery(id));

        var model = _mapper.Map<SalesPipelineViewModel>(pipeline);

        return View(model);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new CreateSalesPipelineViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        CreateSalesPipelineViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var id = await _mediator.Send(
            new CreateSalesPipelineCommand(model.Name));

        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var pipeline = await _mediator.Send(
            new GetSalesPipelineByIdQuery(id));

        var model = _mapper.Map<EditSalesPipelineViewModel>(pipeline);

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        EditSalesPipelineViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _mediator.Send(
            new UpdateSalesPipelineCommand(
                model.Id,
                model.Name));

        return RedirectToAction(
            nameof(Details),
            new { id = model.Id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(
            new DeleteSalesPipelineCommand(id));

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult CreateStage(Guid pipelineId)
    {
        return View(new CreateSalesPipelineStageViewModel
        {
            SalesPipelineId = pipelineId
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateStage(
        CreateSalesPipelineStageViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _mediator.Send(
            new CreateSalesPipelineStageCommand(
                model.SalesPipelineId,
                model.Name,
                model.Order,
                model.Probability));

        return RedirectToAction(
            nameof(Details),
            new { id = model.SalesPipelineId });
    }

    [HttpGet]
    public async Task<IActionResult> EditStage(Guid id)
    {
        var stage = await _mediator.Send(
            new GetSalesPipelineStageByIdQuery(id));

        var model = _mapper.Map<EditSalesPipelineStageViewModel>(stage);

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditStage(
        EditSalesPipelineStageViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _mediator.Send(
            new UpdateSalesPipelineStageCommand(
                model.Id,
                model.Name,
                model.Order,
                model.Probability));

        return RedirectToAction(
            nameof(Details),
            new { id = model.SalesPipelineId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteStage(
        Guid id,
        Guid pipelineId)
    {
        await _mediator.Send(
            new DeleteSalesPipelineStageCommand(id));

        return RedirectToAction(
            nameof(Details),
            new { id = pipelineId });
    }
}