using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UtahPlanners.POC.Domain;

namespace UtahPlanners.POC.Repository
{
    public interface ILookupRepository<T>
        where T : LookupValue
    {
        List<T> GetAllLookupValues();
        Guid CreateLookupValue(T lookupValue);
        bool UpdateLookupValue(Guid id, string description);
        bool DeleteLookupValue(Guid id);
    }
}