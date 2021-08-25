using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using Day08.Model;
using Day08.WebApi.Controllers;

namespace Day08.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static ContainerBuilder GenerateBuilder()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule(new Service.DIModule());
            builder.RegisterModule(new Repository.DIModule());

            return builder;
        }

        //TODO
        /*
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
         */
        protected void Application_Start()
        {
            ContainerBuilder builder = GenerateBuilder();
            var container = builder.Build();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
