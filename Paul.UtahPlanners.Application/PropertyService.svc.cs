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

        public List<PropertiesIndex> GetAllIndecies()
        {
            return GetIndecies(null, null);
        }

        public List<PropertiesIndex> GetIndecies(IndexFilter filter, IndexSort sort)
        {
            using (var unit = _factory.CreateUnitOfWork())
            {
                var repo = unit.CreateIndexRepository();
                return repo.GetIndecies(filter, sort);
            }
        }

        public List<Property> GetAllProperties()
        {
            using (var unit = _factory.CreateUnitOfWork())
            {
                var repo = unit.CreatePropertyRepository(_settings);
                var props = repo.GetAllProperties();
                return props;
            }
        }

        public Property GetProperty(int id)
        {
            using (var unit = _factory.CreateUnitOfWork())
            {
                var repo = unit.CreatePropertyRepository(_settings);
                var prop = repo.Get(id);
                return prop;
            }
        }

        public bool SaveProperty(Property dtoProp)
        {
            var result = false;
            try
            {
                using (var unit = _factory.CreateUnitOfWork())
                {
                    var repo = unit.CreatePropertyRepository(_settings);
                    if (dtoProp.id > 0)
                    {
                        var ctxProp = repo.Get(dtoProp.id);
                        UpdateProperty(ctxProp, dtoProp);
                    }
                    else
                    {
                        repo.Add(dtoProp);
                    }
                    unit.Commit();
                    result = true;
                }
            }
            catch (Exception e)
            {
                // TODO: Logging
            }
            return result;
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

        private void UpdateProperty(Property ctxProp, Property dtoProp)
        {
            // Map address attributes
            ctxProp.Address.street1 = dtoProp.Address.street1;
            ctxProp.Address.street2 = dtoProp.Address.street2;
            ctxProp.Address.city = dtoProp.Address.city;
            ctxProp.Address.state = dtoProp.Address.state;
            ctxProp.Address.zip = dtoProp.Address.zip;
            ctxProp.Address.country = dtoProp.Address.country;

            // Map general attributes
            ctxProp.density = dtoProp.density;
            ctxProp.area = dtoProp.area;
            ctxProp.units = dtoProp.units;
            ctxProp.yearBuilt = dtoProp.yearBuilt;
            ctxProp.walkscore = dtoProp.walkscore;
            ctxProp.twoFiftySingleFam = dtoProp.twoFiftySingleFam;
            ctxProp.twoFiftyApts = dtoProp.twoFiftyApts;
            ctxProp.typeCode = dtoProp.typeCode;
            ctxProp.streetCode = dtoProp.streetCode;
            ctxProp.socioEcon = dtoProp.socioEcon;
            ctxProp.streetSaftey = dtoProp.streetSaftey;
            ctxProp.buildingEnclosure = dtoProp.buildingEnclosure;
            ctxProp.commonAreas = dtoProp.commonAreas;
            ctxProp.streetConn = dtoProp.streetConn;
            ctxProp.streetWalk = dtoProp.streetWalk;
            ctxProp.neighCondition = dtoProp.neighCondition;
            ctxProp.notes = dtoProp.notes;

            // Map hidden attributes
            ctxProp.adminNotes = dtoProp.adminNotes;
            ctxProp.walkscoreNotes = dtoProp.walkscoreNotes;
            ctxProp.notFinished = dtoProp.notFinished;
        }
    }
}
