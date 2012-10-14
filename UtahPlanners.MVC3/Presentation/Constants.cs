using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtahPlanners.MVC3.Presentation
{
    public static class Constants
    {
        public static readonly Dictionary<int, string> LookupTypesDictionary = new Dictionary<int, string>
        {
            { 0, "Property Type" },
            { 1, "Street Type" },
            { 2, "Socio-Econ Type" }
        };
        public static readonly Dictionary<int, string> LookupCodesDictionary = new Dictionary<int, string>
        {
            { 0, "" },
        };
    }
}