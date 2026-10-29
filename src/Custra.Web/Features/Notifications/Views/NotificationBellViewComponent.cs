using Custra.Application.Notifications.Queries.GetUnreadNotificationCount;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Custra.Web.Features.Notifications.Views;

public class NotificationBellViewComponent : ViewComponent
{
    private readonly IMediator _mediator;

    public NotificationBellViewComponent(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var count = await _mediator.Send(
            new GetUnreadNotificationCountQuery());

        return View(count);
    }
}