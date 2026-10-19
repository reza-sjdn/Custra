using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Notifications;

public record NotificationMessage(
    Guid RecipientUserId,
    string Title,
    string Message,
    string? Link = null);