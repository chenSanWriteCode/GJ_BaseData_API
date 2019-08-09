using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GJ_BaseData_API.Entity
{
    public class Station
    {
        public int stationId { get; set; }
        public string stationName { get; set; }
        public string gpsX { get; set; }
        public string gpsY { get; set; }
    }
}