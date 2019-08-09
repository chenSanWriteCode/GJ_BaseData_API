using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GJ_BaseData_API.Dao;
using GJ_BaseData_API.Entity;
using GJ_BaseData_API.Infrastructure;

namespace GJ_BaseData_API.Service
{
    public interface ILineStationService
    {
        Task<Result<List<LineStation>>> getLineStationByUDID(int UDID);
        Task<Result<List<LineStation>>> getLineStationByLineId(int lineId);
    }
}
