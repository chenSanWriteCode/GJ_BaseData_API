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
    public class GJ_DriverController : ApiController
    {
        [Dependency]
        public IDriverService service { get; set; }

        public async Task<IHttpActionResult> Get()
        {
            var result = await service.getDriversByLineId(null);
            return Ok(result);
        }

        public async Task<IHttpActionResult> Get(string id)
        {
            var result = await service.getDriverById(id);
            return Ok(result);
        }
        [Route("api/GJ_Driver/GetByLineId/{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetByLineId(int id)
        {
            var result = await service.getDriversByLineId(id);
            return Ok(result);
        }

        [Route("api/GJ_Driver/GetByBusId1/{id}")]
        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> GetByBusId(int id)
        {
            var result = await service.getDriverByBusId(id);
            return Ok(result);
        }
    }
}
