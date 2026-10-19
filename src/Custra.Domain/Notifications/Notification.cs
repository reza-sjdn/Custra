using Custra.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Domain.Notifications;

public class Notification : OrganizationOwnedEntity
{
    public Guid RecipientUserId { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public string Message { get; private set; } = string.Empty;

    public string? Link { get; private set; }

    public DateTime? ReadAt { get; private set; }

    public bool IsRead => ReadAt.HasValue;

    private Notification() { }

    public Notification(
        Guid recipientUserId,
        string title,
        string message,
        string? link = null)
    {
        if (recipientUserId == Guid.Empty)
            throw new ArgumentException("Recipient user ID is required.");

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required.");

        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentException("Message is required.");

        RecipientUserId = recipientUserId;
        Title = title.Trim();
        Message = message.Trim();
        Link = link;
    }

    public void MarkAsRead()
    {
        ReadAt ??= DateTime.UtcNow;
    }
}