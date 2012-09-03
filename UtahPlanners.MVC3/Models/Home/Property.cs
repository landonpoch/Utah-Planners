using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtahPlanners.MVC3.Models.Home
{
    public class Property
    {
        public int Id { get; set; }
        public Address Address { get; set; }
        public List<int> PictureIds { get; set; }
        public int SecondaryPictureId { get; set; }
        public string PropertyType { get; set; }
        public int Score { get; set; }
        public string StreetSafety { get; set; }
        public string BuildingEnclosure { get; set; }
        public string CommonAreas { get; set; }
        public string StreetConnectivity { get; set; }
        public string StreetWalkability { get; set; }
        public int Walkscore { get; set; }
        public string NeighborhoodCondition { get; set; }
        public int TwoFiftySingleFamily { get; set; }
        public int TwoFiftyApartments { get; set; }
        public double Density { get; set; }
        public int Area { get; set; }
        public int Units { get; set; }
        public string StreetType { get; set; }
        public int YearBuilt { get; set; }
        public string SocioEcon { get; set; }
        public string Notes { get; set; }
    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}