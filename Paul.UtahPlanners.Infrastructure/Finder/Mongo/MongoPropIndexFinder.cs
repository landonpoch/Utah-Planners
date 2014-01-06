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
using UtahPlanners.Infrastructure.DAO;
using UtahPlanners.Infrastructure.Shared;

namespace UtahPlanners.Infrastructure.Finder.Mongo
{
    public class MongoPropIndexFinder : IPropertiesIndexFinder
    {
        private MongoDatabase _db;
        private PropertyContext _context;

        public MongoPropIndexFinder(MongoDatabase db, PropertyContext context)
        {
            _db = db;
            _context = context;
        }

        public List<PropertyIndexDTO> FindIndecies(IndexFilter filter, IndexSort sort)
        {
            var props = GetIndicies<PropertyIndexDTO>(MapToPropertyIndex);
            return props;
        }

        public List<AdminPropertyIndexDTO> FindAdminIndecies(PropertySort sort)
        {
            var props = GetIndicies<AdminPropertyIndexDTO>(MapToAdminPropertyIndex);
            return props;
        }

        private List<T> GetIndicies<T>(Func<Property, T> mapper)
        {
            var props = _db.GetCollection<Property>(typeof(Property).Name)
                .FindAllAs<Property>();
            var indicies = new List<T>();
            foreach (var prop in props)
                indicies.Add(mapper(prop));
            return indicies;
        }

        private PropertyIndexDTO MapToPropertyIndex(Property prop)
        {
            //return new PropertyIndexDTO
            //{
            //    Id = prop.Id,
            //    City = prop.Address.city,
            //    Density = prop.density.GetValueOrDefault(),
            //    OverallScore = 0,
            //    PropertyTypeDescription = Utils.Convert<PropertyType>(_context, prop.typeCode, Utils.GetDescription),
            //    SocioEconDescription = Utils.Convert<SocioEconCode>(_context, prop.socioEcon, Utils.GetDescription),
            //    StreetTypeDescription = Utils.Convert<StreetType>(_context, prop.streetCode, Utils.GetDescription),
            //    StreetWalkDescription = Utils.Convert<StreetwalkCode>(_context, prop.streetWalk, Utils.GetDescription),
            //    TwoFiftySingleFamily = prop.twoFiftySingleFam.GetValueOrDefault(),
            //    Units = prop.units.GetValueOrDefault(),
            //    Walkscore = prop.walkscore.GetValueOrDefault(),
            //    YearBuilt = prop.yearBuilt.GetValueOrDefault()
            //};
            throw new NotImplementedException();
        }

        private AdminPropertyIndexDTO MapToAdminPropertyIndex(Property prop)
        {
            //return new AdminPropertyIndexDTO
            //{
            //    Id = prop.id,
            //    AdminNotes = prop.adminNotes,
            //    City = prop.Address.city,
            //    Density = prop.density.GetValueOrDefault(),
            //    Description = "Description", // TODO
            //    NotFinished = prop.notFinished,
            //    Units = prop.units.GetValueOrDefault(),
            //    YearBuilt = prop.yearBuilt.GetValueOrDefault()
            //};
            throw new NotImplementedException();
        }
    }
}