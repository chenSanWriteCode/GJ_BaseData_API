using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GJ_BaseData_API.Dao;
using GJ_BaseData_API.Entity;
using GJ_BaseData_API.Infrastructure;
using Unity;

namespace GJ_BaseData_API.Service.ServiceImpl
{
    public class DriverServiceImpl : IDriverService
    {
        [Dependency]
        public IDriverDao dao { get; set; }

        public async Task<Result<DriverClock>> getDriverByBusId(int id)
        {
            return await Task.Factory.StartNew(() => dao.getBusById(id));
        }

        public async Task<Result<Driver>> getDriverById(string id)
        {
            return await Task.Factory.StartNew(() => dao.getDriverById(id));
        }

        public async Task<Result<List<Driver>>> getDriversByLineId(int? lineId)
        {
            return await Task.Factory.StartNew(() => dao.getDriversByLineId(lineId));
        }
    }
}