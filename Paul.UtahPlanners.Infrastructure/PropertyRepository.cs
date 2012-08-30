using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtahPlanners.Infrastructure
{
    public class PropertyRepository
    {

        PropertiesDB _db;

        #region Constructors

        public PropertyRepository()
            :this(new PropertiesDB())
        {
        }

        public PropertyRepository(PropertiesDB db)
        {
            _db = db;
        }

        #endregion

        #region Public Members

        public IQueryable<PropertiesIndex> GetAllPropertiesIndexes()
        {
            return _db.PropertiesIndexes;
        }

        public IQueryable<Property> GetAllProperties()
        {
            return _db.Properties;
        }

        public Property GetPropertyById(int id)
        {
            return (from prop in _db.Properties
                    where prop.id == id
                    select prop).First<Property>();
        }

        public void AddProperty(Property property)
        {
            _db.Properties.AddObject(property);
            _db.SaveChanges();
        }

        public void DeleteProperty(Property property)
        {
            _db.Properties.DeleteObject(property);
            _db.SaveChanges();
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public Weight GetWeights()
        {
            return _db.Weights.FirstOrDefault();
        }

        #endregion
    }
}
