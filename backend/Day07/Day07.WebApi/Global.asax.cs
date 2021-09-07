using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;

namespace Day07.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public IContainer Container { get; set; }
        private static ContainerBuilder GenerateBuilder()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule(new Service.DIModule());
            builder.RegisterModule(new Repository.DIModule());

            return builder;
        }
        protected void Application_Start()
        {
            ContainerBuilder builder = GenerateBuilder();
            Container = builder.Build();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
        }
    }
}
