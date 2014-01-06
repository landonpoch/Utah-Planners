using System.Collections.Generic;

namespace UtahPlanners.Domain.Entity
{
    public class LookupValues
    {
        public List<PropertyType> PropertyTypeLookups { get; set; }
        public List<StreetType> StreetTypeLookups { get; set; }
        public List<SocioEcon> SocioEconLookups { get; set; }
        public List<StreetSafety> StreetSafetyLookups { get; set; }
        public List<BuildingEnclosure> BuildingEnclosureLookups { get; set; }
        public List<CommonAreas> CommonAreaLookups { get; set; }
        public List<StreetConnectivity> StreetConnectivityLookups { get; set; }
        public List<StreetWalkability> StreetWalkabilityLookups { get; set; }
        public List<NeighborhoodCondition> NeighborhoodConditionLookups { get; set; }
    }

    #region Lookup Value Classes
    public abstract class LookupValue : Aggregate
    {
        public LookupValue(string description)
        {
            Description = description;
        }

        public string Description { get; private set; }
    }

    public class PropertyType : LookupValue
    {
        public PropertyType(string description)
            : base(description)
        {
        }
    }

    public class StreetType : LookupValue
    {
        public StreetType(string description)
            : base(description)
        {
        }
    }

    public class SocioEcon : LookupValue
    {
        public SocioEcon(string description)
            : base(description)
        {
        }
    }

    public class StreetSafety : LookupValue
    {
        public StreetSafety(string description)
            : base(description)
        {
        }
    }

    public class BuildingEnclosure : LookupValue
    {
        public BuildingEnclosure(string description)
            : base(description)
        {
        }
    }

    public class CommonAreas : LookupValue
    {
        public CommonAreas(string description)
            : base(description)
        {
        }
    }

    public class StreetConnectivity : LookupValue
    {
        public StreetConnectivity(string description)
            : base(description)
        {
        }
    }

    public class StreetWalkability : LookupValue
    {
        public StreetWalkability(string description)
            : base(description)
        {
        }
    }

    public class NeighborhoodCondition : LookupValue
    {
        public NeighborhoodCondition(string description)
            : base(description)
        {
        }
    }
    #endregion
}
