using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using UtahPlanners.Domain;

namespace UtahPlanners.Infrastructure
{
    public class InfrastructureNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWorkFactory>().To<UnitOfWorkFactory>();
        }
    }
}
