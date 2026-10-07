using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Models;

public sealed record LookupItemDto(
    Guid Id,
    string Name);