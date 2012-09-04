using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace UtahPlanners.Domain
{
    public class IndexFilter
    {
        public int? PropertyId { get; set; }
        public string City { get; set; }
        public int? PropertyType { get; set; }
        public Range<double?> DensityRange { get; set; }
        public Range<int?> AreaRange { get; set; }
        public Range<int?> UnitRange { get; set; }
        public int? StreetType { get; set; }
        public Range<int?> YearBuiltRange { get; set; }
        public int? SocioEconType { get; set; }
        public Range<int?> ScoreRange { get; set; }
        public int? StreetSafetyType { get; set; }
        public int? BuildingEnclosureType { get; set; }
        public int? CommonAreasType { get; set; }
        public int? StreetConnectivityType { get; set; }
        public int? StreetWalkabilityType { get; set; }
        public Range<int?> WalkscoreRange { get; set; }
        public int? NeighborhoodConditionType { get; set; }
        public Range<int?> TwoFiftySingleFamilyRange { get; set; }
        public Range<int?> TwoFiftyApartmentsRange { get; set; }
    }

    /// <summary>
    /// Use this type to set ranges for filtering index records
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DataContract(IsReference = true)]
    public class Range<T>
    {


        [DataMember]
        public T LowValue { get; set; }
        [DataMember]
        public T HighValue { get; set; }
    }
}
