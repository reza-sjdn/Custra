using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tasks.DTOs;

public record TaskReminderDto(
    Guid TaskId,
    Guid AssigneeUserId,
    string Title,
    DateTime DueAt);