using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Web.Http;
using GJ_BaseData_API.Filter;
using GJ_BaseData_API.Infrastructure;
using GJ_BaseData_API.Models;
using GJ_BaseData_API.Service;
using Unity;

namespace GJ_BaseData_API.Controllers
{
    public class GJ_ChangedDataController : ApiController
    {
        [Dependency]
        public IChangedDataService service { get; set; }

        [HttpGet, ModelValidation, Route("API/GJ_ChangedData/Bus")]
        public async Task<IHttpActionResult> GetChangedData_Bus([FromUri]TimeQueryModel condition)
        {
            Result<List<string>> result = new Result<List<string>>();
            if (condition == null)
            {
                result.addError("参数为空");
                return Content(System.Net.HttpStatusCode.Accepted, result);
            }
            result = await service.getBusChangedData(condition);
            return Ok(result);
        }

        [HttpGet, ModelValidation, Route("API/GJ_ChangedData/Line")]
        public async Task<IHttpActionResult> GetChangedData_Line([FromUri]TimeQueryModel condition)
        {
            Result<List<string>> result = new Result<List<string>>();
            if (condition == null)
            {
                result.addError("参数为空");
                return Content(System.Net.HttpStatusCode.Accepted, result);
            }
            result = await service.getLineChangedData(condition);
            return Ok(result);
        }

        [HttpGet, ModelValidation, Route("API/GJ_ChangedData/LineUD")]
        public async Task<IHttpActionResult> GetChangedData_LineUD([FromUri]TimeQueryModel condition)
        {
            Result<List<string>> result = new Result<List<string>>();
            if (condition == null)
            {
                result.addError("参数为空");
                return Content(System.Net.HttpStatusCode.Accepted, result);
            }
            result = await service.getLineUDChangedData(condition);
            return Ok(result);
        }

        [HttpGet, ModelValidation, Route("API/GJ_ChangedData/LineStation")]
        public async Task<IHttpActionResult> GetChangedData_LineStation([FromUri]TimeQueryModel condition)
        {
            Result<List<string>> result = new Result<List<string>>();
            if (condition == null)
            {
                result.addError("参数为空");
                return Content(System.Net.HttpStatusCode.Accepted, result);
            }
            result = await service.getLineStationChangedData(condition);
            return Ok(result);
        }
    }
}
