using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain.Contract.Repository;
using UtahPlanners.Domain.Entity;
using UtahPlanners.Infrastructure.DAO;

namespace UtahPlanners.Infrastructure.Repository
{
    public class LookupValueRepository<T> : ILookupValueRepository<T>
        where T : LookupValue
    {
        private PropertyContext _context;

        public LookupValueRepository(PropertyContext context)
        {
            _context = context;
        }

        #region ILookupRepository<T> Members

        public T GetLookupValue(Guid id)
        {
            return _context.Set<T>().Find(id);
        }

        public List<T> GetAllLookupValues()
        {
            return _context.Set<T>().ToList();
        }

        public void AddLookupValue(T lookupValue)
        {
            _context.Set<T>().Add(lookupValue);
        }

        public void RemoveLookupValue(Guid id)
        {
            _context.Set<T>().Remove(GetLookupValue(id));
        }

        #endregion
    }
}
