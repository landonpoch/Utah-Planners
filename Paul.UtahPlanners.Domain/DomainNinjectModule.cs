using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;

namespace UtahPlanners.Domain
{
    public class DomainNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IConfigSettings>().To<ConfigSettings>().InSingletonScope(); // Global configuration cache
        }
    }
}
