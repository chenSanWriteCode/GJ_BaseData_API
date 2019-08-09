using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GJ_BaseData_API.Entity
{
    public class Line
    {
        public int lineId { get; set; }
        public string lineName { get; set; }

        public int departId { get; set; }
    }
}