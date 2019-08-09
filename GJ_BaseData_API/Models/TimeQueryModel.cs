using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GJ_BaseData_API.Models
{
    public class TimeQueryModel
    {
        [Required(AllowEmptyStrings = false)]
        public string startTime { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string endTime { get; set; }
    }
}