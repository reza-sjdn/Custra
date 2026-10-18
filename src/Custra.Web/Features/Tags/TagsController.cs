using AutoMapper;
using Custra.Application.Tags.Commands.AssignTag;
using Custra.Application.Tags.Commands.CreateTag;
using Custra.Application.Tags.Commands.DeleteTag;
using Custra.Application.Tags.Commands.RemoveTagAssignment;
using Custra.Application.Tags.Commands.UpdateTag;
using Custra.Application.Tags.Queries.GetAssignedTags;
using Custra.Application.Tags.Queries.GetTagById;
using Custra.Application.Tags.Queries.GetTagLookup;
using Custra.Application.Tags.Queries.GetTags;
using Custra.Application.Tags.Queries.GetTagUsages;
using Custra.Web.Features.Tags.Services;
using Custra.Web.Features.Tags.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Custra.Web.Features.Tags;


[Authorize]
public class TagsController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public TagsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index(
        string? search,
        int page = 1,
        int pageSize = 10)
    {
        var result = await _mediator.Send(
            new GetTagsQuery(search, page, pageSize));

        var model = new TagListViewModel
        {
            Search = search,
            Page = result.Page,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount,
            Items = _mapper.Map<IReadOnlyList<TagViewModel>>(
                result.Items)
        };

        return View(model);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var dto = await _mediator.Send(new GetTagByIdQuery(id));
        var usages = await _mediator.Send(new GetTagUsagesQuery(id));

        if (dto is null)
            return NotFound();

        var viewModel = _mapper.Map<TagViewModel>(dto);
        viewModel.Usages = usages;

        return View(viewModel);
    }

    [Authorize]
    public IActionResult Create()
    {
        return View(new TagFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TagFormViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var id = await _mediator.Send(
            new CreateTagCommand(model.Name, model.Color));

        return RedirectToAction(nameof(Details), new { id });
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var dto = await _mediator.Send(new GetTagByIdQuery(id));

        if (dto is null)
            return NotFound();

        var model = _mapper.Map<TagFormViewModel>(dto);

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(TagFormViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _mediator.Send(
            new UpdateTagCommand(model.Id, model.Name, model.Color));

        return RedirectToAction(nameof(Details), new { id = model.Id });
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var dto = await _mediator.Send(new GetTagByIdQuery(id));

        if (dto is null)
            return NotFound();

        return View(_mapper.Map<TagViewModel>(dto));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _mediator.Send(new DeleteTagCommand(id));

        return RedirectToAction(nameof(Index));
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Assign(
        string recordType,
        Guid recordId,
        Guid tagId)
    {
        if (!IsSupportedRecordType(recordType) ||
            recordId == Guid.Empty ||
            tagId == Guid.Empty)
        {
            return BadRequest();
        }

        await _mediator.Send(
            new AssignTagCommand(recordType, recordId, tagId));

        return RedirectToRecordDetails(recordType, recordId);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveAssignment(
        string recordType,
        Guid recordId,
        Guid tagId)
    {
        if (!IsSupportedRecordType(recordType) ||
            recordId == Guid.Empty ||
            tagId == Guid.Empty)
        {
            return BadRequest();
        }

        await _mediator.Send(
            new RemoveTagAssignmentCommand(recordType, recordId, tagId));

        return RedirectToRecordDetails(recordType, recordId);
    }

    private static bool IsSupportedRecordType(string recordType) =>
        recordType is "Customer" or "Contact" or "Lead" or "Opportunity";

    private IActionResult RedirectToRecordDetails(
        string recordType,
        Guid recordId)
    {
        var controller = recordType switch
        {
            "Customer" => "Customers",
            "Contact" => "Contacts",
            "Lead" => "Leads",
            "Opportunity" => "Opportunities",
            _ => throw new ArgumentException("Unsupported record type.")
        };

        return RedirectToAction("Details", controller, new { id = recordId });
    }

}
