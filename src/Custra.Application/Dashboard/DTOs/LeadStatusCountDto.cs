using Custra.Domain.Leads;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Dashboard.DTOs;

public sealed record LeadStatusCountDto(
    LeadStatus Status,
    int Count);