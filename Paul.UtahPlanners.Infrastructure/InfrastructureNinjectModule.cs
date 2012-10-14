using Ninject.Modules;
using UtahPlanners.Domain.Contract.UnitOfWork;
using UtahPlanners.Infrastructure.UnitOfWork;
using UtahPlanners.Domain.Contract.Service;
using UtahPlanners.Infrastructure.Service;
using UtahPlanners.Infrastructure.Repository;
using UtahPlanners.Domain.Contract.Repository;

namespace UtahPlanners.Infrastructure
{
    public class InfrastructureNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWorkFactory>().To<UnitOfWorkFactory>();
            Bind<IEmailService>().To<EmailService>();
            Bind(typeof(ILookupRepository<>)).To(typeof(LookupRepository<>));
        }
    }
}
