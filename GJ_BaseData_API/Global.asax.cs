using System;
using System.Timers;
using System.Web.Http;
using GJ_BaseData_API.Job;

namespace GJ_BaseData_API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            string l4net = Server.MapPath("~/Web.config");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(l4net));
            GJ_Driver_Job driverJob = new GJ_Driver_Job();
            driverJob.start();
        }

        
    }
}
