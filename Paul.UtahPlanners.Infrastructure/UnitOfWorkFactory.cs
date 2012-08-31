using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain;

namespace UtahPlanners.Infrastructure
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {

        #region IUnitOfWorkFactory Members

        public IUnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork(new PropertiesDB());
        }

        #endregion
    }
}
