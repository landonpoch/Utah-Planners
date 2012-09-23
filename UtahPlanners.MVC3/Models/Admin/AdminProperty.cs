using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UtahPlanners.MVC3.PropertyService;

namespace UtahPlanners.MVC3.Models.Admin
{
    public class AdminProperty
    {
        public Property Property { get; set; }
        
        public LookupValues LookupValues { get; set; }
        public string PropertyType { get; set; }
        public string StreetType { get; set; }
        public string SocioEcon { get; set; }
        public string StreetSafety { get; set; }
        public string BuildingEnclosure { get; set; }
        public string CommonAreas { get; set; }
        public string StreetConnectivity { get; set; }
        public string StreetWalkability { get; set; }
        public string NeighborhoodCondition { get; set; }
    }
}