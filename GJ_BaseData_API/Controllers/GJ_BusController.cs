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
    public class GJ_BusController : ApiController
    {
        [Dependency]
        public IBusService service { get; set; }


        public async Task<IHttpActionResult> Get()
        {
            var result = await service.getBusByLineId(null);
            return Ok(result);
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            
            var result = await service.getBusById(id);
            return Ok(result);
        }
        [Route("api/GJ_Bus/GetByLineId/{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetByLineId(int id)
        {
            var result = await service.getBusByLineId(id);
            return Ok(result);
        }
    }
}
