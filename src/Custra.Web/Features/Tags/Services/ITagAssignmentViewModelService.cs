using Custra.Web.Features.Tags.ViewModels;

namespace Custra.Web.Features.Tags.Services;

public interface ITagAssignmentViewModelService
{
    Task<TagAssignmentViewModel> PopulateAsync(
        string recordType, Guid recordId);
}
