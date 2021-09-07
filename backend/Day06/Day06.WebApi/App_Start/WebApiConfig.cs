using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Day06.Repository;
using Day06.Repository.Common;
using Day06.Service;
using Day06.Service.Common;
using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Diagnostics;

namespace Day06.WebApi
{
    public static class WebApiConfig
    {
        public static IContainer Container { get; set; }


        private static void InitializeContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            Debug.WriteLine(Assembly.GetExecutingAssembly());

            builder.RegisterType<SongService>().As<ISongService>();
            builder.RegisterType<AlbumService>().As<IAlbumService>();

            builder.RegisterType<SongRepository>().As<ISongRepository>();
            builder.RegisterType<AlbumRepository>().As<IAlbumRepository>();


            Container = builder.Build();
        }

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            InitializeContainer();

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(Container);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}"
            );
        }
    }
}
