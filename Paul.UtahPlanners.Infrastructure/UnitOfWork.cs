using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain;

namespace UtahPlanners.Infrastructure
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

        public IPropertyRepository CreatePropertyRepository(IConfigSettings settings)
        {
            return new PropertyRepository(_context, settings);
        }

        public IPropertiesIndexRepository CreateIndexRepository()
        {
            return new PropertiesIndexRepository(_context);
        }

        public IPictureRepository CreatePictureRepository()
        {
            return new PictureRepository(_context.Pictures);
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
