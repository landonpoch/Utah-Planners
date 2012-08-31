﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtahPlanners.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IPropertyRepository CreatePropertyRepository();
        IPropertiesIndexRepository CreateIndexRepository();
        void Commit();
    }
}
