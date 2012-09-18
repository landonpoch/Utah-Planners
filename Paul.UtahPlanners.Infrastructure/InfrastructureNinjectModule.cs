using Ninject.Modules;
using UtahPlanners.Domain.Contract.UnitOfWork;
using UtahPlanners.Infrastructure.UnitOfWork;
using UtahPlanners.Domain.Contract.Service;
using UtahPlanners.Infrastructure.Service;

namespace UtahPlanners.Infrastructure
{
    public class InfrastructureNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWorkFactory>().To<UnitOfWorkFactory>();
            Bind<IEmailService>().To<EmailService>();
        }
    }
}
