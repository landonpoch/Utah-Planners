using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtahPlanners.Domain.Entity;

namespace UtahPlanners.Domain.Contract.Persistence
{
    public interface IPersistentRepository<T>
        where T : Aggregate
    {
        T Get(Guid id);
        List<T> Find(Func<T, bool> query);
        T Insert(T aggregate);
        bool Update(Guid id, T aggregate);
        bool Remove(Guid id);
    }
}
