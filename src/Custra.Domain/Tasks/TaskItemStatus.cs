using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Domain.Tasks;

public enum TaskItemStatus
{
    NotStarted = 1,
    InProgress = 2,
    Completed = 3,
    Cancelled = 4
}