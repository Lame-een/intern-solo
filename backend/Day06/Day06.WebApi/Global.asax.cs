using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Day06.Service.Common;
using Autofac;
using System.Web.Routing;

namespace Day06.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
