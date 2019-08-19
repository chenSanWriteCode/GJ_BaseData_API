using System;
using System.Timers;
using System.Web.Http;
using GJ_BaseData_API.Job;
using Quartz;
using Quartz.Impl;

namespace GJ_BaseData_API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            string l4net = Server.MapPath("~/Web.config");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(l4net));

            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            var driverClockJob = JobBuilder.Create<GJ_DriverColck_Job>().Build();
            var driverJob = JobBuilder.Create<GJ_Driver_Job>().Build();

            var driverClockTrigger = TriggerBuilder.Create().WithSimpleSchedule(m => m.WithIntervalInSeconds(60).RepeatForever()).StartNow().Build();
            var driverTrigger = TriggerBuilder.Create().WithSimpleSchedule(m => m.WithIntervalInSeconds(60).RepeatForever()).StartNow().Build();

            scheduler.ScheduleJob(driverClockJob, driverClockTrigger);
            scheduler.ScheduleJob(driverJob, driverTrigger);
            //Scheduler();
        }
        private void Scheduler()
        {
            var factory = new StdSchedulerFactory();
            //new System.Collections.Specialized.NameValueCollection()
            //{
            //    { "quartz.plugin.xml.type","Quartz.Plugin.Xml.XMLSchedulingDataProcessorPlugin,Quartz"},
            //        { "quartz.plugin.xml.fileNames","~/Quartz_Job.xml" }
            //}
            IScheduler scheduler = factory.GetScheduler();
            
            scheduler.Start();
        }


    }
}
