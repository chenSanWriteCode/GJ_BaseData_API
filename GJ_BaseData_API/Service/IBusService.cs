using System.Collections.Generic;
using System.Threading.Tasks;
using GJ_BaseData_API.Entity;
using GJ_BaseData_API.Infrastructure;

namespace GJ_BaseData_API.Service
{
    public interface IBusService
    {
        Task<Result<List<Bus>>> getBusByLineId(int? lineId);
        Task<Result<Bus>> getBusById(int id);
    }
}
