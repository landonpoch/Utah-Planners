using System.Linq;
using UtahPlanners.Domain.Contract.Repository;
using UtahPlanners.Domain.Entity;
using UtahPlanners.Infrastructure.DAO;

namespace UtahPlanners.Infrastructure.Repository
{
    public class ConfigRepository : IConfigRepository
    {
        private PropertyContext _context;

        public ConfigRepository(PropertyContext context)
        {
            _context = context;
        }

        #region IConfigRepository Members

        public Weights GetWeights()
        {
            return _context.Weights.FirstOrDefault();
        }

        public LookupValues GetLookupValues()
        {
            return new LookupValues
            {
                PropertyTypeLookups = _context.PropertyTypeLookups.ToList(),
                StreetTypeLookups = _context.StreetTypeLookups.ToList(),
                SocioEconLookups = _context.SocioEconLookups.ToList(),
                StreetSafetyLookups = _context.StreetSafetyLookups.ToList(),
                BuildingEnclosureLookups = _context.BuildingEnclosureLookups.ToList(),
                CommonAreaLookups = _context.CommonAreaLookups.ToList(),
                StreetConnectivityLookups = _context.StreetConnectivityLookups.ToList(),
                StreetWalkabilityLookups = _context.StreetWalkabilityLookups.ToList(),
                NeighborhoodConditionLookups = _context.NeighborhoodConditionLookups.ToList()
            };
        }

        #endregion
    }
}
