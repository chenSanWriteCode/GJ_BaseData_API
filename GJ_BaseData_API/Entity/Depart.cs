using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GJ_BaseData_API.Entity
{
    public class Depart
    {
        public int departId { get; set; }
        public string departName { get; set; }
        public int highLevel { get; set; }
        public int departLevel { get; set; }
    }
}