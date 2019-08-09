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
    public class BusServiceImpl : IBusService
    {
        [Dependency]
        public IBusDao  dao { get; set; }
        public async Task<Result<Bus>> getBusById(int id)
        {
            return await Task.Factory.StartNew(() => dao.getBusById(id));
        }

        public async Task<Result<List<Bus>>> getBusByLineId(int? lineId)
        {
            return await Task.Factory.StartNew(() => dao.getBusByLineId(lineId));
        }
    }
}