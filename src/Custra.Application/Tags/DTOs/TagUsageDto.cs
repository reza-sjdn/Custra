using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Tags.DTOs;

public record TagUsageDto(
    string RecordType,
    Guid RecordId,
    string RecordName);