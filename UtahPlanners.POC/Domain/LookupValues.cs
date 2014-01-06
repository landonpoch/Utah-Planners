using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtahPlanners.POC.Domain
{
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
            :base(description)
        {
        }
    }

    public class StreetCode : LookupValue
    {
        public StreetCode(string description)
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
}