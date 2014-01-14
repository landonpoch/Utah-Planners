using MongoDB.Driver;
using UtahPlanners.Domain.Contract.Persistence;
using UtahPlanners.Domain.Entity;
using UtahPlanners.Infrastructure.DAO;
using UtahPlanners.Infrastructure.Repository.Mongo;

namespace UtahPlanners.Infrastructure.UnitOfWork
{
    public class PersistenceFactory : IPersistenceFactory
    {
        private const string MongoDbName = "UtahPlanners";
        private MongoClient _mongoClient;

        private MongoClient MongoClient
        {
            get
            {
                if (_mongoClient == null)
                    _mongoClient = new MongoClient();
                return _mongoClient;
            }
        }

        #region IUnitOfWorkFactory Members

        public IUnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork(new PropertyContext(), new MongoClient());
        }

        public IPersistentRepository<T> CreatePersistentRepository<T>() 
            where T : Aggregate
        {
            return new MongoPersistentRepository<T>(MongoClient.GetServer().GetDatabase(MongoDbName).GetCollection<T>(typeof(T).Name));
        }

        #endregion
    }
}
