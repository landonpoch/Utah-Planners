[assembly: WebActivator.PreApplicationStartMethod(typeof(Paul.UtahPlanners.Application.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(Paul.UtahPlanners.Application.App_Start.NinjectWebCommon), "Stop")]

namespace Paul.UtahPlanners.Application.App_Start
{
    using System;
    using System.Linq;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using System.Collections.Generic;
    using System.Reflection;
    using Paul.UtahPlanners.Application.Service;
    using Paul.UtahPlanners.Application.Contract;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            // Gets custom assemblies from the solution and loads them into the kernel
            IEnumerable<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .ToList()
                .Where(a => a.FullName.Contains("UtahPlanners"));
            kernel.Load(assemblies);

            kernel.Bind<IMembershipService>().To<DefaultMembershipService>();
            kernel.Bind<IProfileService>().To<DefaultProfileService>();
            kernel.Bind<IRoleService>().To<DefaultRoleService>();
            kernel.Bind<ILogger>().To<Logger>();
        }
    }
}
