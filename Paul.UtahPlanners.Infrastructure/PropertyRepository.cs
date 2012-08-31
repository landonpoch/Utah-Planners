using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain;
using System.Data.Entity;

namespace UtahPlanners.Infrastructure
{
    public class PropertyRepository : IPropertyRepository
    {
        private DbSet<Property> _props;

        public PropertyRepository(DbSet<Property> props)
        {
            _props = props;
        }

        #region IPropertyRepository Members

        public Property Get(int id)
        {
            return _props.FirstOrDefault(p => p.id == id);
        }

        public List<Property> GetAll()
        {
            return _props.ToList();
        }

        #endregion
    }
}
