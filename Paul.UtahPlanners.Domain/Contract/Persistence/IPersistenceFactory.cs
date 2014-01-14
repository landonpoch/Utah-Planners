using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain.Entity;

namespace UtahPlanners.Domain.Contract.Persistence
{
    public interface IPersistenceFactory
    {
        IUnitOfWork CreateUnitOfWork();
        IPersistentRepository<T> CreatePersistentRepository<T>()
            where T : Aggregate;
    }
}
