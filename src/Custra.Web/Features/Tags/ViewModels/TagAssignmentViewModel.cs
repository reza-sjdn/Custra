using Custra.Application.Tags.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Custra.Web.Features.Tags.ViewModels;


public class TagAssignmentViewModel
{
    public string RecordType { get; set; } = string.Empty;
    public Guid RecordId { get; set; }

    public IReadOnlyList<TagAssignmentDto> AssignedTags { get; set; }
        = Array.Empty<TagAssignmentDto>();

    public IReadOnlyList<SelectListItem> AvailableTags { get; set; }
        = Array.Empty<SelectListItem>();
}
