using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GJ_BaseData_API.Entity
{
    public class LineUD
    {
        public int lineId { get; set; }
        public int UDID { get; set; }
        public string UDName { get; set; }
        /// <summary>
        /// 上下行 东方电子是1下行，此处转换过
        /// 1 上行
        /// 2 下行
        /// </summary>
        public int UD { get; set; }
        public float ticketPrice { get; set; }
        public string firstTime { get; set; }
        public string lastTime { get; set; }

    }
}