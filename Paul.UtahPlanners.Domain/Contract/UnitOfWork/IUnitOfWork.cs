using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain.Contract.Repository;
using UtahPlanners.Domain.Contract.Service;

namespace UtahPlanners.Domain.Contract.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IPropertyRepository CreatePropertyRepository(IConfigSettings settings);
        IPropertiesIndexRepository CreateIndexRepository();
        IPictureRepository CreatePictureRepository();
        IConfigRepository CreateConfigRepository();
        void Commit();
    }
}
