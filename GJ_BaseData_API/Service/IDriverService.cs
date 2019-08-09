using System.Collections.Generic;
using System.Threading.Tasks;
using GJ_BaseData_API.Entity;
using GJ_BaseData_API.Infrastructure;


namespace GJ_BaseData_API.Service
{
    public interface IDriverService
    {
        Task<Result<List<Driver>>> getDriversByLineId(int? lineId);
        Task<Result<Driver>> getDriverById(string id);
        Task<Result<DriverClock>> getDriverByBusId(int id);
    }
}
