using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using UtahPlanners.Domain.Contract.Repository;
using UtahPlanners.Domain.Entity;

namespace UtahPlanners.Infrastructure.Repository.Mongo
{
    public class MongoPropertyRepo : IPropertyRepository
    {
        private MongoCollection _props;

        public MongoPropertyRepo(MongoDatabase db)
        {
            _props = db.GetCollection<Property>(typeof(Property).Name); ;
        }

        public void Add(Property property)
        {
            _props.Insert(property);
        }

        public Property Get(Guid id)
        {
            return _props.FindOneAs<Property>(Query<Property>.EQ(p => p.Id, id));
        }

        public void Remove(Property property)
        {
            _props.Remove(Query<Property>.EQ(p => p.Id, property.Id));
        }
    }
}
