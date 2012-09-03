using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtahPlanners.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IPropertyRepository CreatePropertyRepository(IConfigSettings settings);
        IPropertiesIndexRepository CreateIndexRepository();
        IPictureRepository CreatePictureRepository();
        void Commit();
    }
}
