using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using Day09.Model;
using Day09.WebApi.Controllers;

using System.Diagnostics;

namespace Day09.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static ContainerBuilder GenerateBuilder()
        {
            ContainerBuilder builder = new ContainerBuilder();

            Debug.WriteLine("\n\n");
            Debug.WriteLine("\n\n");
            Debug.WriteLine(Assembly.GetExecutingAssembly());
            Debug.WriteLine("\n\n");
            Debug.WriteLine("\n\n");

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule(new Service.DIModule());
            builder.RegisterModule(new Repository.DIModule());

            return builder;
        }

        private static IMapper GenerateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Album, AlbumController.AlbumREST>();
                cfg.CreateMap<Song, SongController.SongREST>();
            }
            );

            config.AssertConfigurationIsValid();

            return config.CreateMapper();
        }

        protected void Application_Start()
        {
            ContainerBuilder builder = GenerateBuilder();
            IMapper mapper = GenerateMapper();
            builder.Register(c => mapper).As<IMapper>().InstancePerLifetimeScope();

            var container = builder.Build();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
