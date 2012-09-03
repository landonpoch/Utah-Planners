using System;
using System.Collections.Generic;
using UtahPlanners.Domain;

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

        public List<global::UtahPlanners.Domain.PropertiesIndex> GetIndex()
        {
            using (var unit = _factory.CreateUnitOfWork())
            {
                var repo = unit.CreateIndexRepository();
                return repo.GetAll();
            }
        }

        public global::UtahPlanners.Domain.Property GetProperty(int id)
        {
            using (var unit = _factory.CreateUnitOfWork())
            {
                var repo = unit.CreatePropertyRepository(_settings);
                return repo.Get(id);
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

        #endregion
    }
}
