[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MarketPlace.Web6.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(MarketPlace.Web6.App_Start.NinjectWebCommon), "Stop")]

namespace MarketPlace.Web6.App_Start
{
    using System;
    using System.Reflection;
    using System.Web;
    using System.Web.Hosting;
    using DependancyResolver;
    using ErrorHandlerLibrary.ExceptionsNS;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
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
            kernel.Bind<IAuthenticationManager>().ToMethod(x => HttpContext.Current.GetOwinContext().Authentication).InRequestScope();
            kernel.Bind<ApplicationUserManager>().ToMethod(ctx => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()).InRequestScope();
            //kernel.Bind<HttpContext>().ToMethod(c => HttpContext.Current).InTransientScope();
            //kernel.Bind<HttpContextBase>().ToMethod(c => new HttpContextWrapper(HttpContext.Current)).InTransientScope();
            kernel.Load(new ApplicationModule ());
        }        
    }
}
