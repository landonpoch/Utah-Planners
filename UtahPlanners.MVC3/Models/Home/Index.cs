using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtahPlanners.MVC3.Models.Home
{
    public class IndexModel
    {
        public List<Index> Records { get; set; }
        public IndexFilter Filter { get; set; }
        public IndexSort Sort { get; set; }
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

    }

    public class IndexSort
    {

    }
}