using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UtahPlanners.POC.Domain;

namespace UtahPlanners.POC.Repository.Mappings
{
    public class MongoMappings
    {
        public void InitializeMappings()
        {
            BsonClassMap.RegisterClassMap<Aggregate>(map =>
            {
                map.AutoMap();
                map.MapIdProperty(a => a.Id);
                //map.IdMemberMap.SetIdGenerator(ObjectIdGenerator.Instance); // Can't really do this if you also want to support EF
                map.IdMemberMap.SetIdGenerator(GuidGenerator.Instance);
            });

            BsonClassMap.RegisterClassMap<Property>(map =>
            {
                map.AutoMap();
                //map.MapCreator(p => new Property(p.Name));
            });
        }
    }
}