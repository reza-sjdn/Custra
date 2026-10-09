using Custra.Application.Dashboard.Queries.GetDashboard;
using Custra.Web.Features.Dashboard.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Custra.Web.Features.Dashboard;

public sealed class DashboardController : Controller
{
    private readonly IMediator _mediator;

    public DashboardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Index(
        CancellationToken cancellationToken)
    {
        var dashboard = await _mediator.Send(
            new GetDashboardQuery(),
            cancellationToken);

        var viewModel = new DashboardViewModel
        {
            Summary = dashboard.Summary,
            LeadsByStatus = dashboard.LeadsByStatus,
            OpportunitiesByStage = dashboard.OpportunitiesByStage,
            RecentActivities = dashboard.RecentActivities,
            UpcomingActivities = dashboard.UpcomingActivities
        };

        return View(viewModel);
    }
}