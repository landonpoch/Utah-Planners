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
        private IConfigSettings _settings;

        public PropertyRepository(DbSet<Property> props, IConfigSettings settings)
        {
            _props = props;
            _settings = settings;
        }

        #region IPropertyRepository Members

        public Property Get(int id)
        {
            var property = _props.FirstOrDefault(p => p.id == id);
            property.Weights = _settings.Weights;
            return property;
        }

        public List<Property> GetAll()
        {
            var properties = _props.ToList();
            properties.ForEach(p => p.Weights = _settings.Weights);
            return properties;
        }

        #endregion
    }
}
