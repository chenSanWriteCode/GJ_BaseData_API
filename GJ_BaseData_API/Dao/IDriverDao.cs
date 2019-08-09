using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GJ_BaseData_API.Entity;
using GJ_BaseData_API.Infrastructure;

namespace GJ_BaseData_API.Dao
{
    public interface IDriverDao
    {
        Result<List<Driver>> getDriversByLineId(int? lineId);
        Result<Driver> getDriverById(string id);
        Result<DriverClock> getBusById(int id);
    }
}
