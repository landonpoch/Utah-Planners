using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UtahPlanners.POC.Domain;
using UtahPlanners.POC.Repository.Mappings;

namespace UtahPlanners.POC.Repository
{
    public class EfPropertyRepository : IPropertyRepository
    {
        private PropertyContext _context;

        public EfPropertyRepository(PropertyContext context)
        {
            _context = context;
        }

        public Guid Add(Property property)
        {
            
            _context.Properties.Add(property);
            _context.SaveChanges();
            return property.Id;
        }

        public List<Property> GetAllProperties()
        {
            return _context.Properties.ToList();
        }
    }
}