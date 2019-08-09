using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GJ_BaseData_API.Dao;
using GJ_BaseData_API.Entity;
using GJ_BaseData_API.Infrastructure;
using Unity;

namespace GJ_BaseData_API.Service.ServiceImpl
{
    public class LineStationServiceImpl : ILineStationService
    {
        [Dependency]
        public ILineStationDao dao { get; set; }
        public async Task<Result<List<LineStation>>> getLineStationByLineId(int lineId)
        {
            return await Task.Factory.StartNew(() => dao.getLineStationByLineId(lineId));
        }

        public async Task<Result<List<LineStation>>> getLineStationByUDID(int UDID)
        {
            return await Task.Factory.StartNew(() => dao.getLineStationByUDID(UDID));
        }
    }
}