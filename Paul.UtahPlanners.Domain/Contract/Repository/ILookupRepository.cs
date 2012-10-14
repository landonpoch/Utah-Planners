﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtahPlanners.Domain.Contract.Repository
{
    public interface ILookupRepository<TEntity>
    {
        TEntity GetLookupValue(int id);
        List<TEntity> GetAllLookupValues();
        void AddLookupValue(TEntity lookupValue);
    }
}
