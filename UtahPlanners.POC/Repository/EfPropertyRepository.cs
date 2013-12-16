using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UtahPlanners.POC.Domain;

namespace UtahPlanners.POC.Repository
{
    public class EfPropertyRepository : IPropertyRepository
    {
        private object _context;

        public EfPropertyRepository(object context)
        {
            _context = context;
        }

        public object Add(Property property)
        {
            throw new NotImplementedException();
        }

        public List<Property> GetAllProperties()
        {
            throw new NotImplementedException();
        }
    }
}