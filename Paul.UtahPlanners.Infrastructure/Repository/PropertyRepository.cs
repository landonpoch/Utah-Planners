using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UtahPlanners.Domain.Contract.Repository;
using UtahPlanners.Domain.Contract.Service;
using UtahPlanners.Domain.Entity;
using UtahPlanners.Infrastructure.DAO;
using UtahPlanners.Infrastructure.Shared;

namespace UtahPlanners.Infrastructure.Repository
{
    public class PropertyRepository : IPropertyRepository
    {
        private PropertiesDB _context;

        public PropertyRepository(PropertiesDB context)
        {
            _context = context;
        }

        #region IPropertyRepository Members

        public void Add(Property property)
        {
            _context.Properties.Add(property);
        }

        public Property Get(int id)
        {
            return _context.Properties
                .Include(p => p.Address)
                .Where(p => p.id == id)
                .FirstOrDefault();
        }

        public void Remove(Property property)
        {
            _context.Properties.Remove(property);
        }

        #endregion
    }
}
