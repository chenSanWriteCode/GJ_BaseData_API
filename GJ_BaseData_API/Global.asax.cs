using System;
using System.Timers;
using System.Web.Http;
using GJ_BaseData_API.Job;

namespace GJ_BaseData_API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static GJ_Job driverClockJob = new GJ_DriverColck_Job();
        private static GJ_Job driverJob =new GJ_Driver_Job();
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            string l4net = Server.MapPath("~/Web.config");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(l4net));
            driverClockJob.start();
            driverJob.start();
        }

        
    }
}
