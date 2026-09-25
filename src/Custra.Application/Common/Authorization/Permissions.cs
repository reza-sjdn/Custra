using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Authorization;

public static class Permissions
{
    public static class Customers
    {
        public const string View = "Customers.View";
        public const string Create = "Customers.Create";
        public const string Update = "Customers.Update";
        public const string Delete = "Customers.Delete";
    }
}