using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GJ_BaseData_API.Entity
{
    public class Driver
    {
        public string driverNo { get; set; }
        public string driverName { get; set; }
        public int lineId { get; set; }
        /// <summary>
        /// 1 正常 0删除
        /// </summary>
        public int isWork { get; set; }
    }
}