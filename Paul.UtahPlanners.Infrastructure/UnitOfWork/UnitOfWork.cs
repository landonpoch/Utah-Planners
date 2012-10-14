using UtahPlanners.Domain.Contract.Repository;
using UtahPlanners.Domain.Contract.Service;
using UtahPlanners.Domain.Contract.UnitOfWork;
using UtahPlanners.Infrastructure.DAO;
using UtahPlanners.Infrastructure.Repository;
using UtahPlanners.Domain.Contract.Finder;
using UtahPlanners.Infrastructure.Finder;

namespace UtahPlanners.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private PropertiesDB _context;

        public UnitOfWork(PropertiesDB context)
        {
            _context = context;

            // Needed to be able to use the POCO classes over the wire
            _context.Configuration.LazyLoadingEnabled = false;
            _context.Configuration.ProxyCreationEnabled = false;
        }

        #region IUnitOfWork Members

        public IPropertyFinder CreatePropertyFinder()
        {
            return new PropertyFinder(_context);
        }

        public IPropertiesIndexFinder CreateIndexFinder()
        {
            return new PropertiesIndexFinder(_context);
        }

        public IPictureFinder CreatePictureFinder()
        {
            return new PictureFinder(_context.Pictures);
        }

        public IPropertyRepository CreatePropertyRepository(IConfigSettings settings)
        {
            return new PropertyRepository(_context, settings);
        }

        public IConfigRepository CreateConfigRepository()
        {
            return new ConfigRepository(_context);
        }

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
