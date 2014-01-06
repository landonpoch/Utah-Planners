using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UtahPlanners.POC.Domain;

namespace UtahPlanners.POC.Repository
{
    public class MongoLookupRepository<T> : ILookupRepository<T>
        where T : LookupValue
    {
        private MongoDatabase _db;

        public MongoLookupRepository(MongoDatabase db)
        {
            _db = db;
        }

        public List<T> GetAllLookupValues() 
        {
            return GetCollection<T>()
                .FindAll()
                .ToList();
        }
        
        public Guid CreateLookupValue(T lookupValue)
        {
            GetCollection<T>()
                .Insert(lookupValue);
            return lookupValue.Id;
        }

        public bool UpdateLookupValue(Guid id, string description)
        {
            var result = GetCollection<T>()
                .Update(Query<T>.EQ(l => l.Id, id), Update<T>.Set(l => l.Description, description));
            return result.Ok;
        }

        public bool DeleteLookupValue(Guid id)
        {
            var result = GetCollection<T>()
                .Remove(Query<T>.EQ(l => l.Id, id));
            return result.Ok;
        }

        private MongoCollection<T> GetCollection<T>()
        {
            return _db.GetCollection<T>(typeof(T).Name);
        }
    }
}