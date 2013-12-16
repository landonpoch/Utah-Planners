using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtahPlanners.POC.Domain
{
    public class Property : Aggregate
    {
        public Property(string name)
        {
            name = Name;
        }

        public string Name { get; private set; }
    }
}