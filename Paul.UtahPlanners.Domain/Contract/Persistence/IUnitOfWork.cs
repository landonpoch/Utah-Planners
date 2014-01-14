using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain.Contract.Repository;
using UtahPlanners.Domain.Contract.Service;
using UtahPlanners.Domain.Contract.Finder;
using UtahPlanners.Domain.Entity;

namespace UtahPlanners.Domain.Contract.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        // Query Model
        IPropertyFinder CreatePropertyFinder();
        IPropertiesIndexFinder CreateIndexFinder();

        // Command Model
        IPropertyRepository CreatePropertyRepository();
        IConfigRepository CreateConfigRepository();
        ILookupValueRepository<T> CreateLookupValueRepository<T>()
            where T : LookupValue;
        
        void Commit();
    }
}
