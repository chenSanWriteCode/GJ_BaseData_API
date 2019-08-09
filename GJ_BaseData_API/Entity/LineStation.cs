using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GJ_BaseData_API.Entity
{
    public class LineStation
    {
        /// <summary>
        /// 上下行ID
        /// </summary>
        public int UDID { get; set; }
        public int stationIndex { get; set; }
        public int stationId { get; set; }
        public string stationName { get; set; }
        public string gpsX { get; set; }
        public string gpsY  { get; set; }
        /// <summary>
        /// 0:真实站点;1:虚拟站点
        /// </summary>
        public int isVirtual { get; set; }

    }
}