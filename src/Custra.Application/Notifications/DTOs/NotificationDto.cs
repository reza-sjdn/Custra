using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Notifications.DTOs;

public record NotificationDto(
    Guid Id,
    string Title,
    string Message,
    string? Link,
    DateTime CreatedAt,
    DateTime? ReadAt)
{
    public bool IsRead => ReadAt.HasValue;
}