using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtahPlanners.Domain.Contract.UnitOfWork
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork CreateUnitOfWork();
    }
}
