using System;
using System.Web;
using ContosoUniversity.DAL;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Template.Authentication.PasswordHashing;
using Template.Authentication.Persistance;
using Template.EFMappingImplementation;
using Template.Mapping;
using Template.MappingContract;
using Template.Persistance.EntityFrameworkImpl.Authentication;
using Template.PersistanceContract.Membership;
using Template.Website;
using Template.Website.ViewModels.Mapping;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace Template.Website
{
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
            kernel.Bind<TemplateDBContext>().ToSelf().InRequestScope();
            kernel.Bind<MappingBaseline>().ToSelf();
            kernel.Bind<InjectedMappingConfiguration>().To<DomainToEntityFrameworkMappingConfig>();
            kernel.Bind<InjectedMappingConfiguration>().To<DomainToViewModelMappingConfig>();
            kernel.Bind<IMembershipPersistance>().To<MembershipPersistanceEFImpl>();
            kernel.Bind<IRollPersistance>().To<RollPersistanceEFImpl>();
            kernel.Bind<IPermissionPersistance>().To<PermissionPersistanceEFImpl>();
            kernel.Bind<IPasswordHashing>().To<BCryptHashImpl>();
        }        
    }
}
