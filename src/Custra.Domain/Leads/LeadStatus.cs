using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Domain.Leads;

public enum LeadStatus
{
    New = 1,
    Contacted = 2,
    Qualified = 3,
    Disqualified = 4
}
