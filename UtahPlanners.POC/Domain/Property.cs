using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtahPlanners.POC.Domain
{
    public class Property : Aggregate
    {
        private Property() { }

        public Property(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}