using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;

namespace GJ_BaseData_API.Job
{
    public abstract class GJ_Job
    {
        protected Timer timerJob = new Timer();

        public virtual void start()
        {
            timerJob.Start();
        }
        public virtual void end()
        {
            timerJob.Stop();
        }
    }


}