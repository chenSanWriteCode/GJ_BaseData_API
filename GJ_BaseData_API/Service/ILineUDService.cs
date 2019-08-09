using System.Collections.Generic;
using System.Threading.Tasks;
using GJ_BaseData_API.Entity;
using GJ_BaseData_API.Infrastructure;

namespace GJ_BaseData_API.Service
{
    public interface ILineUDService
    {
        Task<Result<List<LineUD>>> getLIneUDByLineId(int? lineId);
        Task<Result<LineUD>> getLineUDById(int id);
    }
}
