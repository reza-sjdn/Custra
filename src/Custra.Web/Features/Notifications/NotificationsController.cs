using Custra.Application.Notifications.Commands.MarkNotificationAsRead;
using Custra.Application.Notifications.Queries.GetLatestNotifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Custra.Web.Features.Notifications;

[Authorize]
public class NotificationsController : Controller
{
    private readonly IMediator _mediator;

    public NotificationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var notifications = await _mediator.Send(
            new GetLatestNotificationsQuery(100));

        return View(notifications);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarkAsRead(Guid id)
    {
        await _mediator.Send(new MarkNotificationAsReadCommand(id));

        return RedirectToAction(nameof(Index));
    }
}