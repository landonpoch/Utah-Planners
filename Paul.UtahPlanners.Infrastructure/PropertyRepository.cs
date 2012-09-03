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
        private PropertiesDB _context;
        private IConfigSettings _settings;

        public PropertyRepository(PropertiesDB context, IConfigSettings settings)
        {
            _context = context;
            _settings = settings;
        }

        #region IPropertyRepository Members

        public Property Get(int id)
        {
            var property = _context.Properties.FirstOrDefault(p => p.id == id);
            property.Weights = _settings.Weights;
            return property;
        }

        public KeyValuePair<int, int> GetShowcaseProperty()
        {
            var count = _context.Pictures.Where(p => p.frontPage == 1)
                .Count();

            var index = new Random().Next(count);

            var result = _context.Pictures.Where(p => p.frontPage == 1)
                .OrderBy(p => p.id)
                .Skip(index)
                .Select(p => new { PropertyId = p.property_id.Value, PictureId = p.id })
                .FirstOrDefault();
            return new KeyValuePair<int, int>(result.PropertyId, result.PictureId);
        }

        public List<Property> GetAll()
        {
            var properties = _context.Properties.ToList();
            properties.ForEach(p => p.Weights = _settings.Weights);
            return properties;
        }

        #endregion
    }
}
