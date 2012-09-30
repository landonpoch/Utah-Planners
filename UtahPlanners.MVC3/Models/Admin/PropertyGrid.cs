using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtahPlanners.MVC3.Models.Admin
{
    public class PropertyGrid
    {
        public List<PropertyService.Property> Properties { get; set; }
        public Dictionary<int, string> SortOptions { get; set; }
    }
}