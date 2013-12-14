using UtahPlanners.Domain.Contract.Repository;
using UtahPlanners.Domain.Contract.Service;
using UtahPlanners.Domain.Contract.UnitOfWork;
using UtahPlanners.Infrastructure.DAO;
using UtahPlanners.Infrastructure.Repository;
using UtahPlanners.Domain.Contract.Finder;
using UtahPlanners.Infrastructure.Finder;
using MongoDB.Driver;
using UtahPlanners.Infrastructure.Repository.Mongo;
using UtahPlanners.Infrastructure.Finder.Mongo;

namespace UtahPlanners.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private const string MongoDbName = "UtahPlanners";

        private PropertiesDB _context;
        private MongoDatabase _mongoDb;

        public UnitOfWork(PropertiesDB context, MongoClient mongoClient)
        {
            _context = context;

            // Needed to be able to use the POCO classes over the wire
            _context.Configuration.LazyLoadingEnabled = false;
            _context.Configuration.ProxyCreationEnabled = false;

            _mongoDb = mongoClient.GetServer().GetDatabase(MongoDbName);
        }

        #region IUnitOfWork Members

        #region Finders
        
        public IPropertyFinder CreatePropertyFinder()
        {
            //return new PropertyFinder(_context);
            return new MongoPropertyFinder(_mongoDb, new MongoPropertyRepo(_mongoDb));
        }

        public IPropertiesIndexFinder CreateIndexFinder()
        {
            //return new PropertiesIndexFinder(_context);
            return new MongoPropIndexFinder(_mongoDb);
        }

        public IPictureFinder CreatePictureFinder()
        {
            return new PictureFinder(_context.Pictures);
        }

        #endregion

        #region Repositories
        
        public IPropertyRepository CreatePropertyRepository()
        {
            //return new PropertyRepository(_context);
            return new MongoPropertyRepo(_mongoDb);
        }

        public IPictureRepository CreatePictureRepository()
        {
            return new PictureRepository(_context);
        }

        public IConfigRepository CreateConfigRepository()
        {
            return new ConfigRepository(_context);
        }

        public ILookupValueRepository<T> CreateLookupValueRepository<T>()
            where T : class
        {
            return new LookupValueRepository<T>(_context);
        }

        #endregion

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        #endregion
    }
}
