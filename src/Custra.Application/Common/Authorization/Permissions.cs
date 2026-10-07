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

    public static class Contacts
    {
        public const string View = "Contacts.View";
        public const string Create = "Contacts.Create";
        public const string Update = "Contacts.Update";
        public const string Delete = "Contacts.Delete";
    }

    public static class Leads
    {
        public const string View = "Leads.View";
        public const string Create = "Leads.Create";
        public const string Update = "Leads.Update";
        public const string Delete = "Leads.Delete";
    }

    public static class Pipelines
    {
        public const string View = "Pipelines.View";
        public const string Create = "Pipelines.Create";
        public const string Update = "Pipelines.Update";
        public const string Delete = "Pipelines.Delete";
    }

    public static class Opportunities
    {
        public const string View = "Opportunities.View";
        public const string Create = "Opportunities.Create";
        public const string Update = "Opportunities.Update";
        public const string Delete = "Opportunities.Delete";
    }

}