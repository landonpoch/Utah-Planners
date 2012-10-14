using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain.Contract.Repository;
using UtahPlanners.Domain.Contract.Service;
using UtahPlanners.Domain.Contract.Finder;

namespace UtahPlanners.Domain.Contract.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        // Query Model
        IPropertyFinder CreatePropertyFinder();
        IPropertiesIndexFinder CreateIndexFinder();
        IPictureFinder CreatePictureFinder();

        // Command Model
        IPropertyRepository CreatePropertyRepository();
        IConfigRepository CreateConfigRepository();
        
        void Commit();
    }
}
