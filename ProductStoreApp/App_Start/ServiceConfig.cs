[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ProductStoreApp.App_Start.ServiceConfig), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(ProductStoreApp.App_Start.ServiceConfig), "Stop")]

namespace ProductStoreApp.App_Start
{
 
using ClsStore.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Operation.Models;
using ClsStore.Operation;
using Ninject;
using Ninject.Web.Common.WebHost;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject.Web.Common;

    public class ServiceConfig
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        public static IKernel CreateKernel()
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
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IDBContext>().To<ClsDBContext>().InRequestScope();
            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>)).InRequestScope();
            kernel.Bind<IProduct>().To<ProductOperation>();
            kernel.Bind<ICategory>().To<CategoryOperation>();


        }
    }
}