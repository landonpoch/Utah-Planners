using Ninject.Modules;
using UtahPlanners.Domain.Contract.UnitOfWork;
using UtahPlanners.Infrastructure.UnitOfWork;

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
