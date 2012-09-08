using System;
using System.Collections.Generic;
using UtahPlanners.Domain.Contract.Service;
using UtahPlanners.Domain.Contract.UnitOfWork;
using UtahPlanners.Domain.Entity;

namespace Paul.UtahPlanners.Application
{
    public class PropertyService : IPropertyService
    {
        private IUnitOfWorkFactory _factory;
        private IConfigSettings _settings;

        public PropertyService(IUnitOfWorkFactory factory, IConfigSettings settings)
        {
            _factory = factory;
            _settings = settings;
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        #region IPropertyService Members

        public List<global::UtahPlanners.Domain.Entity.PropertiesIndex> GetAllIndecies()
        {
            return GetIndecies(null, null);
        }

        public List<global::UtahPlanners.Domain.Entity.PropertiesIndex> GetIndecies(IndexFilter filter, IndexSort sort)
        {
            using (var unit = _factory.CreateUnitOfWork())
            {
                var repo = unit.CreateIndexRepository();
                return repo.GetIndecies(filter, sort);
            }
        }

        public global::UtahPlanners.Domain.Entity.Property GetProperty(int id)
        {
            using (var unit = _factory.CreateUnitOfWork())
            {
                var repo = unit.CreatePropertyRepository(_settings);
                var prop = repo.Get(id);
                return prop;
            }
        }

        public KeyValuePair<int, int> GetShowcaseProperty()
        {
            using (var unit = _factory.CreateUnitOfWork())
            {
                var repo = unit.CreatePropertyRepository(_settings);
                return repo.GetShowcaseProperty();
            }
        }

        public Picture GetPicture(int id)
        {
            using (var unit = _factory.CreateUnitOfWork())
            {
                var repo = unit.CreatePictureRepository();
                return repo.GetPicture(id);
            }
        }

        public LookupValues GetLookupValues()
        {
            return _settings.LookupValues;
        }

        #endregion
    }
}
