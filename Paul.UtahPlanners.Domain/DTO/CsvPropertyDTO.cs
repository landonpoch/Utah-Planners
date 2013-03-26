using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtahPlanners.Domain.DTO
{
    public class CsvPropertyDTO : PropertyDTO
    {
        public string PropertyType { get; set; }
        public string StreetType { get; set; }
        public string SocioEconType { get; set; }
        public string StreetSafetyCode { get; set; }
        public string BuildingEnclosureCode { get; set; }
        public string CommonAreasCode { get; set; }
        public string StreetConnectivityCode { get; set; }
        public string StreetWalkabilityCode { get; set; }
        public string NeighborhoodConditionCode { get; set; }
    }
}
