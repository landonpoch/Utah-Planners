using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtahPlanners.Domain.Entity
{
    public class Property : Aggregate
    {
        private Property() { }

        public Property(Address address, Guid propertyTypeId)
        {
            Address = address;
            PropertyTypeId = propertyTypeId;
        }

        public Address Address { get; private set; }

        public Guid PropertyTypeId { get; private set; }

        public int? Area { get; set; }

        public int? Density { get; set; }

        public int? Units { get; set; }

        public int? YearBuilt { get; set; }

        public int? Walkscore { get; set; }

        public int? TwoFiftySingleFamily { get; set; }

        public int? TwoFiftyApartments { get; set; }

        public GeoLocation GeoLocation { get; set; }

        public PropertyNotes Notes { get; set; }

        public Guid? StreetTypeId { get; set; }

        public Guid? SocioEconCodeId { get; set; }

        public Guid? StreetSafetyId { get; set; }

        public Guid? BuildingEnclosureId { get; set; }

        public Guid? CommonAreasId { get; set; }

        public Guid? StreetConnectivityId { get; set; }

        public Guid? StreetWalkabilityId { get; set; }

        public Guid? NeighborhoodConditionId { get; set; }
    }

    public class GeoLocation
    {
        public GeoLocation(double lat, double @long)
        {
            Latitude = lat;
            Longitutde = @long;
        }

        public double Latitude { get; private set; }

        public double Longitutde { get; private set; }
    }

    public class PropertyNotes
    {
        public string Notes { get; set; }

        public string AdminNotes { get; set; }

        public string WalkscoreNotes { get; set; }

        public string NotFinished { get; set; }
    }
}
