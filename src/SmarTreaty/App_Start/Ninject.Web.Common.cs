

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SmarTreaty.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(SmarTreaty.App_Start.NinjectWebCommon), "Stop")]

namespace SmarTreaty.App_Start
{
    using System;
    using System.Web;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using SmarTreaty.Business.Data.Helpers;
    using SmarTreaty.Business.Services;
    using SmarTreaty.Common.Core.Helpers.Interfaces;
    using SmarTreaty.Core.Services.Interfaces;

    public static class NinjectWebCommon 
    {
        internal static readonly Bootstrapper bootstrapper = new Bootstrapper();

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
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IDatabaseWorkUnit>().To<DatabaseWorkUnit>().WithConstructorArgument("name=DataContext");
            var databaseWorkUnit = kernel.Get<IDatabaseWorkUnit>();

            kernel.Bind<ICourseService>().To<CourseService>().WithConstructorArgument(databaseWorkUnit);
            kernel.Bind<ICourseGroupService>().To<CourseGroupsService>().WithConstructorArgument(databaseWorkUnit);
            kernel.Bind<IRoleService>().To<RoleService>().WithConstructorArgument(databaseWorkUnit);
            kernel.Bind<ITrainerGroupService>().To<TrainerGroupService>().WithConstructorArgument(databaseWorkUnit);
            kernel.Bind<ITrainerService>().To<TrainerService>().WithConstructorArgument(databaseWorkUnit);
            kernel.Bind<IUserService>().To<UserService>().WithConstructorArgument(databaseWorkUnit);
        }        
    }
}