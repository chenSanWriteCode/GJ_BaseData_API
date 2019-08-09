using System.Collections.Generic;
using System.Threading.Tasks;
using GJ_BaseData_API.Entity;
using GJ_BaseData_API.Infrastructure;

namespace GJ_BaseData_API.Service
{
    public interface ILineService
    {
        Task<Result<List<Line>>> getLineByDepartId(int? id);
        Task<Result<Line>> getLineById(int lineId);
    }
}
