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
        }

        #region IUnitOfWork Members

        public IPropertyRepository CreatePropertyRepository(IConfigSettings settings)
        {
            return new PropertyRepository(_context.Properties, settings);
        }

        public IPropertiesIndexRepository CreateIndexRepository()
        {
            return new PropertiesIndexRepository(_context.PropertiesIndexes);
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
