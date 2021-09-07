using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Day03.App_Code;
using System.Data.SqlClient;

namespace Day03
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DatabaseHandler instance = DatabaseHandler.Instance;

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
