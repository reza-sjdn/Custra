using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tags.DTOs;


public record TagAssignmentDto(
    Guid TagId,
    string Name,
    string Color);
