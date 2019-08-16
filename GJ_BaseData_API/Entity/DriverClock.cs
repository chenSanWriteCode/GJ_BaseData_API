using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GJ_BaseData_API.Entity
{
    public class DriverClock
    {
        public string busNum { get; set; }
        public string driverNo { get; set; }
        public string clockTime { get; set; }
        public int isWork { get; set; }
        public string driverName { get; set; }

    }
}