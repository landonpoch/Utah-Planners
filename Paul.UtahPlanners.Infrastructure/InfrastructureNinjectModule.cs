using Ninject.Modules;
using UtahPlanners.Domain.Contract.Persistence;
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
            Bind<IPersistenceFactory>().To<PersistenceFactory>();
            Bind<IEmailService>().To<EmailService>();
            Bind(typeof(ILookupValueRepository<>)).To(typeof(LookupValueRepository<>));
        }
    }
}
