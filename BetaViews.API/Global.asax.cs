﻿using System.Web.Http;


namespace BetaViews.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {      
            GlobalConfiguration.Configure(WebApiConfig.Register);            
            UnityConfig.RegisterComponents();
        }
    }
}
