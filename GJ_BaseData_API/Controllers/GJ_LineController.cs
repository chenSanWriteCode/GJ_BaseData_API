using System.Threading.Tasks;
using System.Web.Http;
using GJ_BaseData_API.Service;
using Unity;

namespace GJ_BaseData_API.Controllers
{
    public class GJ_LineController : ApiController
    {
        [Dependency]
        public ILineService service { get; set; }
        

        public async Task<IHttpActionResult> Get()
        {
            var result = await service.getLineByDepartId(null);
            return Ok(result);
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            var result = await service.getLineById(id);
            return Ok(result);
        }

        [Route("api/GJ_Line/GetByDepartId/{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetByLineId(int id)
        {
            var result = await service.getLineByDepartId(id);
            return Ok(result);
        }
    }
}
