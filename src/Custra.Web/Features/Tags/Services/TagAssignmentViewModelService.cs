using Custra.Application.Tags.Queries.GetAssignedTags;
using Custra.Application.Tags.Queries.GetTagLookup;
using Custra.Web.Features.Tags.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Custra.Web.Features.Tags.Services;

public class TagAssignmentViewModelService : ITagAssignmentViewModelService
{
    private readonly IMediator _mediator;

    public TagAssignmentViewModelService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<TagAssignmentViewModel> PopulateAsync(
        string recordType,
        Guid recordId)
    {
        var assignedTags = await _mediator.Send(
            new GetAssignedTagsQuery(recordType, recordId));

        var availableTags = await _mediator.Send(
            new GetTagLookupQuery());

        var assignedIds = assignedTags
            .Select(x => x.TagId)
            .ToHashSet();

        return new TagAssignmentViewModel
        {
            RecordType = recordType,
            RecordId = recordId,
            AssignedTags = assignedTags,
            AvailableTags = availableTags
                .Where(x => !assignedIds.Contains(x.Id))
                .Select(x => new SelectListItem(
                    x.Name, x.Id.ToString()))
                .ToList()
        };
    }
}
