﻿using UtahPlanners.Domain.Contract.Repository;
using UtahPlanners.Domain.Contract.Service;
using UtahPlanners.Domain.Contract.UnitOfWork;
using UtahPlanners.Infrastructure.DAO;
using UtahPlanners.Infrastructure.Repository;

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