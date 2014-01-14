using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain.Entity;

namespace UtahPlanners.Domain.Contract.Repository
{
    public interface ILookupValueRepository<T>
        where T : LookupValue
    {
        T GetLookupValue(Guid id);
        List<T> GetAllLookupValues();
        void AddLookupValue(T lookupValue);
        void RemoveLookupValue(Guid id);
    }
}
