using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtahPlanners.Domain.Entity
{
    public class Weights
    {
        public int StreetWalkability { get; set; }
        public int Walkscore { get; set; }
        public int TwoFiftySingleFamily { get; set; }
        public int BuildingEnclosure { get; set; }
        public int StreetConnectivity { get; set; }
        public int CommonAreas { get; set; }
        public int StreetSafety { get; set; }
        public int NeighborhoodCondition { get; set; }
        public int TwoFiftyApartments { get; set; }
    }
}
