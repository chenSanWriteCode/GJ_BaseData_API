using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using GJ_BaseData_API.Service;
using Unity;

namespace GJ_BaseData_API.Controllers
{
    public class GJ_LineStationController : ApiController
    {
        [Dependency]
        public ILineStationService service { get; set; }

        public async Task<IHttpActionResult> Get(int id)
        {
            var result = await service.getLineStationByUDID(id);
            return Ok(result);
        }
        [Route("api/GJ_LineStation/GetByLineId/{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetByLineId(int id)
        {
            var result = await service.getLineStationByLineId(id);
            return Ok(result);
        }
    }
}
