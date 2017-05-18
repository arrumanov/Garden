[assembly: WebActivator.PreApplicationStartMethod(typeof(Garden.WebUI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(Garden.WebUI.App_Start.NinjectWebCommon), "Stop")]

namespace Garden.WebUI.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Garden.Domain.Concrete;

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
            kernel.Bind<IEFDbContext>().To<EFDbContext>();
            kernel.Bind<IUoWEFDbContext>().To<UnitOfWorkEFDbContext>();

            RegisterServices(kernel);
            return kernel;
        }

        // шлюз между классом NinjectDependencyResolver, 
        //добавленным в проект одним из NuGet-пакетов Ninject, 
        //и поддержкой внедрения зависимостей в MVC
        private static void RegisterServices(IKernel kernel)
        {
            System.Web.Mvc.DependencyResolver.SetResolver(new
            Garden.WebUI.Infrastructure.NinjectDependencyResolver(kernel));
        }        
    }
}
