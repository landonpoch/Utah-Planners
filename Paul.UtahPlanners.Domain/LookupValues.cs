using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace UtahPlanners.Domain
{
    public class LookupValues
    {
        public List<PropertyType> PropertyTypes { get; set; }
        public List<StreetType> StreetTypes { get; set; }
        public List<SocioEconCode> SocioEconCodes { get; set; }
        public List<StreetSafteyCode> StreetSafetyCodes { get; set; }
        public List<EnclosureCode> EnclosureCodes { get; set; }
        public List<CommonCode> CommonCodes { get; set; }
        public List<StreetconnCode> StreetconnCodes { get; set; }
        public List<StreetwalkCode> StreetwalkCodes { get; set; }
        public List<NeighborhoodCode> NeighborhoodCodes { get; set; }
    }
}
