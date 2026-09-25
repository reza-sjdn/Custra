using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Exceptions;

public sealed class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException()
        : base("You do not have permission to perform this operation.")
    {
    }
}