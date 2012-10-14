using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtahPlanners.Domain.Contract.Repository
{
    public interface ILookupValueRepository<TEntity>
    {
        TEntity GetLookupValue(int id);
        List<TEntity> GetAllLookupValues();
        void AddLookupValue(TEntity lookupValue);
        void RemoveLookupValue(int id);
    }
}
