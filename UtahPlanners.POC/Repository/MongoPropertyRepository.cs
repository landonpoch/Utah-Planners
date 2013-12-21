using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UtahPlanners.POC.Domain;

namespace UtahPlanners.POC.Repository
{
    public class MongoPropertyRepository : IPropertyRepository
    {
        private MongoDatabase _db;

        public MongoPropertyRepository(MongoDatabase db)
        {
            _db = db;
        }

        public Guid Add(Property property)
        {
            var result = GetProperties()
                .Insert<Property>(property);
            return property.Id;
        }

        public List<Property> GetAllProperties()
        {
            return GetProperties()
                .FindAll()
                .ToList();
        }

        private MongoCollection<Property> GetProperties()
        {
            return _db.GetCollection<Property>(typeof(Property).Name);
        }
    }
}