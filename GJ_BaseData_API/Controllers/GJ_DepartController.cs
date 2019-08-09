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
    public class GJ_DepartController : ApiController
    {
        [Dependency]
        public IDepartService service { get; set; }


        public async Task<IHttpActionResult> Get()
        {
            var result = await service.getDepart();
            return Ok(result);
        }

        public async Task<IHttpActionResult> Get(int id)
        {

            var result = await service.getDepartById(id);
            return Ok(result);
        }
    }
}
