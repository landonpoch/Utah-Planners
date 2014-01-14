using System;
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
    [Serializable]
    public abstract class LookupValue : Aggregate
    {
        public LookupValue(string description)
        {
            Description = description;
        }

        public string Description { get; private set; }
    }

    [Serializable]
    public class PropertyType : LookupValue
    {
        public PropertyType(string description)
            : base(description)
        {
        }
    }

    [Serializable]
    public class StreetType : LookupValue
    {
        public StreetType(string description)
            : base(description)
        {
        }
    }

    [Serializable]
    public class SocioEcon : LookupValue
    {
        public SocioEcon(string description)
            : base(description)
        {
        }
    }
    #endregion

    #region Weighted Lookup Value Classes
    [Serializable]
    public abstract class WeightedLookupValue : LookupValue
    {
        public WeightedLookupValue(string description, int weight)
            : base(description)
        {
            Weight = weight;
        }

        public int Weight { get; private set; }
    }

    [Serializable]
    public class StreetSafety : WeightedLookupValue
    {
        public StreetSafety(string description, int weight)
            : base(description, weight)
        {
        }
    }

    [Serializable]
    public class BuildingEnclosure : WeightedLookupValue
    {
        public BuildingEnclosure(string description, int weight)
            : base(description, weight)
        {
        }
    }

    [Serializable]
    public class CommonAreas : WeightedLookupValue
    {
        public CommonAreas(string description, int weight)
            : base(description, weight)
        {
        }
    }

    [Serializable]
    public class StreetConnectivity : WeightedLookupValue
    {
        public StreetConnectivity(string description, int weight)
            : base(description, weight)
        {
        }
    }

    [Serializable]
    public class StreetWalkability : WeightedLookupValue
    {
        public StreetWalkability(string description, int weight)
            : base(description, weight)
        {
        }
    }

    [Serializable]
    public class NeighborhoodCondition : WeightedLookupValue
    {
        public NeighborhoodCondition(string description, int weight)
            : base(description, weight)
        {
        }
    }
    #endregion
}
