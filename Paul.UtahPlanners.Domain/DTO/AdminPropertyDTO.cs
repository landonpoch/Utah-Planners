using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain.Entity;

namespace UtahPlanners.Domain.DTO
{
    public class AdminPropertyDTO : PropertyDTO
    {
        public int Type { get; set; }
        public int StreetType { get; set; }
        public int SocioEcon { get; set; }
        public int StreetSafety { get; set; }
        public int BuildingEnclosure { get; set; }
        public int CommonAreas { get; set; }
        public int StreetConnectivity { get; set; }
        public int StreetWalkability { get; set; }
        public int NeighborhoodCondition { get; set; }
        public string AdminNotes { get; set; }
        public string WalkscoreNotes { get; set; }
        public string NotFinished { get; set; }
    }
}
