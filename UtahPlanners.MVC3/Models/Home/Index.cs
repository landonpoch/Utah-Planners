using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UtahPlanners.MVC3.Models.Home
{
    public class IndexModel
    {
        public List<Index> Records { get; set; }
        public IndexFilter Filter { get; set; }
        public IndexSort Sort { get; set; }
        public DropDowns DropDowns { get; set; }

        public string PropType { get; set; }
        public string Density { get; set; }
        public string Walkscore { get; set; }
        public bool ResetView { get; set; }
    }

    public class DropDowns
    {
        public SelectList PropertyTypes { get; set; }
        public SelectList StreetTypes { get; set; }
        public SelectList SocioEconCodes { get; set; }
        public SelectList StreetSafetyCodes { get; set; }
        public SelectList EnclosureCodes { get; set; }
        public SelectList CommonCodes { get; set; }
        public SelectList StreetconnCodes { get; set; }
        public SelectList StreetwalkCodes { get; set; }
        public SelectList NeighborhoodCodes { get; set; }
    }

    public class Index
    {
        public int Id { get; set; }
        public string City { get; set; }
        public int Score { get; set; }
        public string PropertyType { get; set; }
        public double Density { get; set; }
        public int Units { get; set; }
        public string YearBuilt { get; set; }
        public string StreetType { get; set; }
        public string StreetWalkDescription { get; set; }
        public int Walkscore { get; set; }
        public string SocioEconDescription { get; set; }
        public int TwoFiftySingleFamily { get; set; }
    }

    public class IndexFilter
    {
        public int? PropertyId { get; set; }
        public string City { get; set; }
        public int? PropertyType { get; set; }
        public List<int> PropertyTypes { get; set; }
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

    public class Range<T>
    {
        public T LowValue { get; set; }
        public T HighValue { get; set; }
    }

    public class IndexSort
    {
        public IndexColumn? Column { get; set; }
        public Direction? Direction { get; set; }
    }

    public enum IndexColumn
    {
        City,
        Id,
        Score,
        Type,
        Density,
        Units,
        Year,
        StreetType,
        Walkability,
        Walkscore,
        SocioEcon,
        TwoFiftySF
    }

    public enum Direction
    {
        Ascending,
        Descending
    }
}