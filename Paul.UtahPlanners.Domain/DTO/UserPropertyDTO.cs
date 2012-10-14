using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain.Entity;

namespace UtahPlanners.Domain.DTO
{
    public class UserPropertyDTO : PropertyDTO
    {
        public int Score { get; set; }
        public string Type { get; set; }
        public string StreetSafety { get; set; }
        public string BuildingEnclosure { get; set; }
        public string CommonAreas { get; set; }
        public string StreetConnectivity { get; set; }
        public string StreetWalkability { get; set; }
        public string NeighborhoodCondition { get; set; }
        public string StreetType { get; set; }
        public string SocioEcon { get; set; }
    }
}
