using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain.Contract.Repository;
using UtahPlanners.Infrastructure.DAO;

namespace UtahPlanners.Infrastructure.Repository
{
    public class LookupValueRepository<TEntity> : ILookupValueRepository<TEntity>
        where TEntity : class
    {
        private PropertiesDB _context;

        public LookupValueRepository(PropertiesDB context)
        {
            _context = context;
        }

        #region ILookupRepository<T> Members

        public TEntity GetLookupValue(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public List<TEntity> GetAllLookupValues()
        {
            return _context.Set<TEntity>().ToList();
        }

        public void AddLookupValue(TEntity lookupValue)
        {
            _context.Set<TEntity>().Add(lookupValue);
        }

        public void RemoveLookupValue(int id)
        {
            _context.Set<TEntity>().Remove(GetLookupValue(id));
        }

        #endregion
    }
}
