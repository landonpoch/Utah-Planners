using Ninject.Modules;
using UtahPlanners.Domain.Contract.Service;
using UtahPlanners.Domain.Services;

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
