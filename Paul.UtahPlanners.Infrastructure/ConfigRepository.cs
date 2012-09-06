using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain;

namespace UtahPlanners.Infrastructure
{
    public class ConfigRepository : IConfigRepository
    {
        private PropertiesDB _context;

        public ConfigRepository(PropertiesDB context)
        {
            _context = context;
        }

        #region IConfigRepository Members

        public Weight GetWeights()
        {
            return _context.Weights.FirstOrDefault();
        }

        public LookupValues GetLookupValues()
        {
            return new LookupValues
            {
                PropertyTypes = _context.PropertyTypes.ToList(),
                StreetTypes = _context.StreetTypes.ToList(),
                SocioEconCodes = _context.SocioEconCodes.ToList(),
                StreetSafetyCodes = _context.StreetSafteyCodes.ToList(),
                EnclosureCodes = _context.EnclosureCodes.ToList(),
                CommonCodes = _context.CommonCodes.ToList(),
                StreetconnCodes = _context.StreetconnCodes.ToList(),
                StreetwalkCodes = _context.StreetwalkCodes.ToList(),
                NeighborhoodCodes = _context.NeighborhoodCodes.ToList()
            };
        }

        #endregion
    }
}
