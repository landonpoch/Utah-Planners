using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtahPlanners.Domain.DTO
{
    public class PropertyIndexDTO
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public int Density { get; set; }
        public int OverallScore { get; set; }
        public string PropertyTypeDescription { get; set; }
        public string SocioEconDescription { get; set; }
        public string StreetTypeDescription { get; set; }
        public string StreetWalkDescription { get; set; }
        public int TwoFiftySingleFamily { get; set; }
        public int Units { get; set; }
        public int Walkscore { get; set; }
        public int YearBuilt { get; set; }
    }
}
