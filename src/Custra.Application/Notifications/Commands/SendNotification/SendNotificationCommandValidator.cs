using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Notifications.Commands.SendNotification;

public class SendNotificationCommandValidator
    : AbstractValidator<SendNotificationCommand>
{
    public SendNotificationCommandValidator()
    {
        RuleFor(x => x.RecipientUserId)
            .NotEmpty();

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Message)
            .NotEmpty()
            .MaximumLength(2000);

        RuleFor(x => x.Link)
            .MaximumLength(500);
    }
}