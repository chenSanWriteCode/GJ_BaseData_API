using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GJ_BaseData_API.Entity
{
    public class Bus
    {
        public int busId { get; set; }
        public string busNum { get; set; }
        public int lineId { get; set; }
        /// <summary>
        /// 是否报废
        /// 1 报废 0 正常
        /// </summary>
        public int isAbandoned { get; set; }
    }
}