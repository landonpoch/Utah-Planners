using System.Collections.Generic;

namespace UtahPlanners.Domain.Entity
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
