using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtahPlanners.Domain.Contract.Repository;
using UtahPlanners.Domain.Entity;

namespace UtahPlanners.Infrastructure.Repository.Mongo
{
    public class MongoConfigRepository : IConfigRepository
    {
        private MongoDatabase _mongoDb;

        public MongoConfigRepository(MongoDatabase mongoDb)
        {
            _mongoDb = mongoDb;
        }

        public Weights GetWeights()
        {
            return _mongoDb.GetCollection<Weights>(typeof(Weights).Name)
                .FindOne();
        }

        public LookupValues GetLookupValues()
        {
            return new LookupValues
            {
                PropertyTypeLookups = GetLookups<PropertyType>(),
                StreetTypeLookups = GetLookups<StreetType>(),
                SocioEconLookups = GetLookups<SocioEcon>(),
                StreetSafetyLookups = GetLookups<StreetSafety>(),
                BuildingEnclosureLookups = GetLookups<BuildingEnclosure>(),
                CommonAreaLookups = GetLookups<CommonAreas>(),
                StreetConnectivityLookups = GetLookups<StreetConnectivity>(),
                StreetWalkabilityLookups = GetLookups<StreetWalkability>(),
                NeighborhoodConditionLookups = GetLookups<NeighborhoodCondition>()
            };
        }

        private List<T> GetLookups<T>()
            where T : LookupValue
        {
            return _mongoDb.GetCollection<T>(typeof(T).Name)
                .FindAll()
                .ToList();
        }
    }
}
