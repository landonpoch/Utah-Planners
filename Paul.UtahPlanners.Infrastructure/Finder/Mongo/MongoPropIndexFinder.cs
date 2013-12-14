using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain.Contract.Finder;
using UtahPlanners.Domain.Contract.Repository;
using UtahPlanners.Domain.DTO;
using UtahPlanners.Domain.Entity;
using UtahPlanners.Infrastructure.Shared;

namespace UtahPlanners.Infrastructure.Finder.Mongo
{
    public class MongoPropIndexFinder : IPropertiesIndexFinder
    {
        private MongoDatabase _db;

        public MongoPropIndexFinder(MongoDatabase db)
        {
            _db = db;
        }

        public List<PropertiesIndex> FindIndecies(IndexFilter filter, IndexSort sort)
        {
            var props = _db.GetCollection<Property>(typeof(Property).Name)
                .FindAllAs<Property>();
            var indicies = new List<PropertiesIndex>();
            foreach (var prop in props)
                indicies.Add(Convert(prop));
            return indicies;
        }

        public List<AdminPropertyIndexDTO> FindAdminIndecies(PropertySort sort)
        {
            throw new NotImplementedException();
        }

        private PropertiesIndex Convert(Property prop)
        {
            return new PropertiesIndex
            {
                id = prop.id,
                city = prop.Address.city,
                density = prop.density.GetValueOrDefault(),
                OverallScore = 0,
                PropertyTypeDescription = Utils.Convert<PropertyType>(_db, prop.typeCode, Utils.GetDescription),
                SocioEconDescription = Utils.Convert<SocioEconCode>(_db, prop.socioEcon, Utils.GetDescription),
                StreetTypeDescription = Utils.Convert<StreetType>(_db, prop.streetCode, Utils.GetDescription),
                StreetWalkDescription = Utils.Convert<StreetwalkCode>(_db, prop.streetWalk, Utils.GetDescription),
                twoFiftySingleFam = prop.twoFiftySingleFam.GetValueOrDefault(),
                units = prop.units.GetValueOrDefault(),
                walkscore = prop.walkscore.GetValueOrDefault(),
                yearBuilt = prop.yearBuilt.GetValueOrDefault()
            };
        }
    }
}